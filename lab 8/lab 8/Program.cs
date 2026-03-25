using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8_AnalysisOfAlgorithms
{
    /// <summary>
    /// Lab 8 – Analysis of Algorithms
    /// CST2550 - Software Engineering and Management
    /// 
    /// This class implements and analyses three core sorting algorithms:
    /// BubbleSort, QuickSort, and MergeSort. It includes performance
    /// measurement, memory profiling, and optimised variants.
    /// </summary>
    public class SortingAlgorithms
    {
        // ============================================================
        // TASK 1: Algorithm Implementation (40 minutes)
        //  

        #region BubbleSort

        /// <summary>
        /// Standard Bubble Sort – O(n²) average and worst case.
        /// Repeatedly steps through the list, compares adjacent elements,
        /// and swaps them if they are in the wrong order.
        /// </summary>
        public void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Swap
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Optimised Bubble Sort – includes an early-exit flag so that
        /// the algorithm stops as soon as a full pass completes with no swaps.
        /// Best case becomes O(n) for already-sorted input.
        /// </summary>
        public void BubbleSortOptimised(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                // If no swaps occurred, the array is already sorted
                if (!swapped) break;
            }
        }

        #endregion

        #region QuickSort

        /// <summary>
        /// QuickSort – O(n log n) average case, O(n²) worst case.
        /// Uses a divide-and-conquer approach with a pivot element.
        /// </summary>
        public void QuickSort(int[] arr)
        {
            QuickSortRecursive(arr, 0, arr.Length - 1);
        }

        private void QuickSortRecursive(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);
                QuickSortRecursive(arr, low, pivotIndex - 1);
                QuickSortRecursive(arr, pivotIndex + 1, high);
            }
        }

        /// <summary>
        /// Lomuto partition scheme – picks last element as pivot.
        /// </summary>
        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }

        /// <summary>
        /// Optimised QuickSort – uses median-of-three pivot selection
        /// and switches to insertion sort for small sub-arrays (≤10 elements).
        /// This avoids the O(n²) worst case on sorted/nearly-sorted data.
        /// </summary>
        public void QuickSortOptimised(int[] arr)
        {
            QuickSortOptimisedRecursive(arr, 0, arr.Length - 1);
        }

        private void QuickSortOptimisedRecursive(int[] arr, int low, int high)
        {
            // For small sub-arrays, insertion sort is faster due to lower overhead
            if (high - low < 10)
            {
                InsertionSort(arr, low, high);
                return;
            }

            if (low < high)
            {
                int pivotIndex = PartitionMedianOfThree(arr, low, high);
                QuickSortOptimisedRecursive(arr, low, pivotIndex - 1);
                QuickSortOptimisedRecursive(arr, pivotIndex + 1, high);
            }
        }

        /// <summary>
        /// Median-of-three pivot selection prevents worst-case behaviour
        /// on sorted or nearly-sorted input.
        /// </summary>
        private int PartitionMedianOfThree(int[] arr, int low, int high)
        {
            int mid = low + (high - low) / 2;

            // Sort low, mid, high to find median
            if (arr[low] > arr[mid]) Swap(arr, low, mid);
            if (arr[low] > arr[high]) Swap(arr, low, high);
            if (arr[mid] > arr[high]) Swap(arr, mid, high);

            // Place median pivot at high-1
            Swap(arr, mid, high - 1);
            int pivot = arr[high - 1];
            int i = low;
            int j = high - 1;

            while (true)
            {
                while (arr[++i] < pivot) { }
                while (arr[--j] > pivot) { }
                if (i >= j) break;
                Swap(arr, i, j);
            }
            Swap(arr, i, high - 1);
            return i;
        }

        /// <summary>
        /// Insertion sort – used by optimised QuickSort for small sub-arrays.
        /// O(n²) overall but very fast for small n due to minimal overhead.
        /// </summary>
        private void InsertionSort(int[] arr, int low, int high)
        {
            for (int i = low + 1; i <= high; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= low && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        #endregion

        #region MergeSort

        /// <summary>
        /// MergeSort – O(n log n) in all cases (best, average, worst).
        /// Stable sort that divides the array in half, sorts each half,
        /// then merges them back together.
        /// </summary>
        public void MergeSort(int[] arr)
        {
            MergeSortRecursive(arr, 0, arr.Length - 1);
        }

        private void MergeSortRecursive(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                MergeSortRecursive(arr, left, mid);
                MergeSortRecursive(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }

        private void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            Array.Copy(arr, left, leftArr, 0, n1);
            Array.Copy(arr, mid + 1, rightArr, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                    arr[k++] = leftArr[i++];
                else
                    arr[k++] = rightArr[j++];
            }

            while (i < n1) arr[k++] = leftArr[i++];
            while (j < n2) arr[k++] = rightArr[j++];
        }

        /// <summary>
        /// Optimised MergeSort – skips merge when halves are already in order,
        /// and uses insertion sort for small sub-arrays.
        /// Also reuses a single auxiliary buffer to reduce memory allocations.
        /// </summary>
        public void MergeSortOptimised(int[] arr)
        {
            int[] aux = new int[arr.Length]; // Single buffer, reused across all merges
            MergeSortOptimisedRecursive(arr, aux, 0, arr.Length - 1);
        }

        private void MergeSortOptimisedRecursive(int[] arr, int[] aux, int left, int right)
        {
            // Switch to insertion sort for small sub-arrays
            if (right - left < 10)
            {
                InsertionSort(arr, left, right);
                return;
            }

            int mid = left + (right - left) / 2;
            MergeSortOptimisedRecursive(arr, aux, left, mid);
            MergeSortOptimisedRecursive(arr, aux, mid + 1, right);

            // Skip merge if already sorted (left half's max ≤ right half's min)
            if (arr[mid] <= arr[mid + 1]) return;

            MergeWithBuffer(arr, aux, left, mid, right);
        }

        private void MergeWithBuffer(int[] arr, int[] aux, int left, int mid, int right)
        {
            Array.Copy(arr, left, aux, left, right - left + 1);

            int i = left, j = mid + 1, k = left;
            while (i <= mid && j <= right)
            {
                if (aux[i] <= aux[j])
                    arr[k++] = aux[i++];
                else
                    arr[k++] = aux[j++];
            }
            while (i <= mid) arr[k++] = aux[i++];
            // No need to copy remaining right half – it's already in place
        }

        #endregion

        // ============================================================
        // TASK 2: Performance Testing (40 minutes)
        // ============================================================

        #region Test Data Generation

        /// <summary>
        /// Generates various test datasets to evaluate algorithm behaviour
        /// under different input conditions.
        /// </summary>
        public static int[] GenerateRandomArray(int size, int seed = 42)
        {
            Random rng = new Random(seed);
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = rng.Next(0, size * 10);
            return arr;
        }

        public static int[] GenerateSortedArray(int size)
        {
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = i;
            return arr;
        }

        public static int[] GenerateReverseSortedArray(int size)
        {
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = size - i;
            return arr;
        }

        public static int[] GenerateNearlySortedArray(int size, int swaps = 10)
        {
            int[] arr = GenerateSortedArray(size);
            Random rng = new Random(42);
            for (int i = 0; i < swaps; i++)
            {
                int a = rng.Next(size);
                int b = rng.Next(size);
                int temp = arr[a];
                arr[a] = arr[b];
                arr[b] = temp;
            }
            return arr;
        }

        public static int[] GenerateDuplicatesArray(int size)
        {
            Random rng = new Random(42);
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = rng.Next(0, 10); // Only 10 distinct values
            return arr;
        }

        #endregion

        #region Performance Measurement

        /// <summary>
        /// Measures execution time of a sorting action using high-resolution Stopwatch.
        /// Runs multiple iterations and returns the average to reduce noise.
        /// </summary>
        public static double MeasureExecutionTime(Action sortAction, int iterations = 3)
        {
            // Warm-up run (JIT compilation)
            sortAction();

            Stopwatch sw = new Stopwatch();
            double totalMs = 0;

            for (int i = 0; i < iterations; i++)
            {
                sw.Restart();
                sortAction();
                sw.Stop();
                totalMs += sw.Elapsed.TotalMilliseconds;
            }

            return totalMs / iterations;
        }

        /// <summary>
        /// Estimates memory usage by measuring GC allocations before and after.
        /// </summary>
        public static long MeasureMemoryUsage(Action sortAction)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long before = GC.GetTotalMemory(true);
            sortAction();
            long after = GC.GetTotalMemory(false);

            return Math.Max(0, after - before);
        }

        #endregion

        #region Compare Algorithms

        /// <summary>
        /// Comprehensive comparison of all sorting algorithms across multiple
        /// dataset types and sizes. Outputs a formatted results table.
        /// </summary>
        public void CompareAlgorithms(int[] data)
        {
            Console.WriteLine($"\n  Array size: {data.Length:N0} elements");
            Console.WriteLine(new string('─', 80));
            Console.WriteLine($"  {"Algorithm",-28} {"Time (ms)",12} {"Memory (bytes)",16} {"Sorted?",10}");
            Console.WriteLine(new string('─', 80));

            var algorithms = new (string Name, Action<int[]> Sort)[]
            {
                ("BubbleSort",           arr => BubbleSort(arr)),
                ("BubbleSort (Optimised)", arr => BubbleSortOptimised(arr)),
                ("QuickSort",            arr => QuickSort(arr)),
                ("QuickSort (Optimised)",  arr => QuickSortOptimised(arr)),
                ("MergeSort",            arr => MergeSort(arr)),
                ("MergeSort (Optimised)",  arr => MergeSortOptimised(arr)),
            };

            foreach (var (name, sort) in algorithms)
            {
                int[] copy = (int[])data.Clone();

                double time = MeasureExecutionTime(() =>
                {
                    int[] fresh = (int[])data.Clone();
                    sort(fresh);
                });

                long memory = MeasureMemoryUsage(() =>
                {
                    int[] fresh = (int[])data.Clone();
                    sort(fresh);
                });

                // Verify correctness
                sort(copy);
                bool sorted = IsSorted(copy);

                Console.WriteLine($"  {name,-28} {time,12:F4} {memory,16:N0} {(sorted ? "✓" : "✗"),10}");
            }

            Console.WriteLine(new string('─', 80));
        }

        #endregion

        // ============================================================
        // TASK 3: Optimisation – Validation & Bottleneck Identification
        // ============================================================

        #region Validation

        /// <summary>
        /// Verifies that an array is sorted in non-decreasing order.
        /// </summary>
        public static bool IsSorted(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[i - 1]) return false;
            }
            return true;
        }

        #endregion

        #region Utility

        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        #endregion

        // ============================================================
        // EXTENSION: Parallel MergeSort
        // ============================================================

        #region Parallel MergeSort

        /// <summary>
        /// Parallel MergeSort – splits work across threads using Task Parallel Library.
        /// Falls back to sequential MergeSort once the sub-array is small enough.
        /// </summary>
        public void ParallelMergeSort(int[] arr)
        {
            int[] aux = new int[arr.Length];
            ParallelMergeSortRecursive(arr, aux, 0, arr.Length - 1, 0);
        }

        private void ParallelMergeSortRecursive(int[] arr, int[] aux, int left, int right, int depth)
        {
            if (right - left < 10)
            {
                InsertionSort(arr, left, right);
                return;
            }

            int mid = left + (right - left) / 2;

            // Only parallelise at top levels to avoid excessive thread overhead
            if (depth < 3)
            {
                Task leftTask = Task.Run(() => ParallelMergeSortRecursive(arr, aux, left, mid, depth + 1));
                Task rightTask = Task.Run(() => ParallelMergeSortRecursive(arr, aux, mid + 1, right, depth + 1));
                Task.WaitAll(leftTask, rightTask);
            }
            else
            {
                ParallelMergeSortRecursive(arr, aux, left, mid, depth + 1);
                ParallelMergeSortRecursive(arr, aux, mid + 1, right, depth + 1);
            }

            if (arr[mid] <= arr[mid + 1]) return;
            MergeWithBuffer(arr, aux, left, mid, right);
        }
        class Program
        {
            static void Main(string[] args)
            {
                SortingAlgorithms sorter = new SortingAlgorithms();

                Console.WriteLine("Lab 8: Analysis of Algorithms");
                Console.WriteLine(new string('=', 50));

                int[] sizes = { 1000, 5000, 10000 };

                foreach (int size in sizes)
                {
                    int[] data = SortingAlgorithms.GenerateRandomArray(size);
                    sorter.CompareAlgorithms(data);
                }

                Console.WriteLine("\nDone. Press any key to exit.");
                Console.ReadKey();
            }
        }












        #endregion
    }
}