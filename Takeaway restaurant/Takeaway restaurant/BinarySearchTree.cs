using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takeaway_restaurant
{
    public class BinarySearchTree
    {
        private BSTNode root;
        public BinarySearchTree()
        {
            root = null;
        }

        //== INSERT==
        // adds a new Item in MenuItem into the BST based on its ID
        //average: 0(log n) | worst: 0(n)
        public void Insert(MenuItem item) 
        {
            root = InsertRecursive(root, item);
        }
        private BSTNode InsertRecursive(BSTNode node, MenuItem item) 
        {
            if (node == null) 
            {
                return new BSTNode(item);
            }

            if (item.Id < node.Data.Id) 
            {
                node.Left = InsertRecursive(node.Left, item);
            }
            else if (item.Id > node.Data.Id)
            {
                node.Right = InsertRecursive(node.Right, item); 
            }
            else
            {
                Console.WriteLine($"Item with Id{item.Id} already exists. Updating.");
                node.Data = item;
            }
            return node;
        }


        //==SEARCH byt ID===
        // Average: 0(log n) | worst: 0(n)

        public MenuItem SearchById(int id)
        {
            BSTNode result = SearchByIdRecursive(root, id);
            return result != null ? result.Data : null;
        }
        private BSTNode SearchByIdRecursive(BSTNode node, int id)
        {
            if (node == null || node.Data.Id == id)
            {
                return node;
            }
            if (id < node.Data.Id)
            {
                return SearchByIdRecursive(node.Left, id);
            }

            return SearchByIdRecursive(node.Right, id);
        }


        //==search by NAME==
        // Time COmplexity: 0(n) - must check every node

        public MenuItem SearchByName(string name)
        {
            return SearchByNameRecursive(root, name);
        }

        private MenuItem SearchByNameRecursive(BSTNode node, string name)
        {
            if (node == null)
            {
                return null;
            }
            if (node.Data.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return node.Data;
            }

            MenuItem found = SearchByNameRecursive(node.Left, name);
            if (found != null) 
            {
                return found;
            }
            return SearchByNameRecursive(node.Right, name);
        }

        //==DELETE==
        //Average: 0(log n) | Worst: 0(n)
        public bool Delete(int id)
        {
            if (SearchById(id)==null)
            {
                return false;
            }
            root = DeleteRecursive(root, id);
            return true;
        }

        private BSTNode DeleteRecursive(BSTNode node, int id)
        {
            if (node == null)
            {
                return null;
            }

            if (id < node.Data.Id)
            {
                node.Left = DeleteRecursive(node.Left, id);
            }
            else if (id > node.Data.Id)
            {
                node.Right = DeleteRecursive(node.Right, id);
            }
            else
            {
                //no children (leaf node)
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                //one child
                if (node.Left == null)
                {
                    return node.Right;
                }
                if (node.Right == null)
                {
                    return node.Left;
                }

                //Two children - find in order successor
                BSTNode successor = FindMin(node.Right);
                node.Data = successor.Data;
                node.Right = DeleteRecursive(node.Right, successor.Data.Id);
            }
            return node;
        }
        private BSTNode FindMin(BSTNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }


        //==in order traversal==
        //displayes all items sorted by Id
        //time complexity: 0(n)

        public void DisplayAll()
        {
            if (root == null)
            {
                Console.WriteLine("No Menu items found");
                return;
            }
            InOrderTraversal(root);
        }

        private void InOrderTraversal(BSTNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.WriteLine(node.Data.ToString());
                Console.WriteLine();
                InOrderTraversal(node.Right);
            }
        }


        //==search by category==
        //time complexity: 0(n)

        public void DisplayByCategory(string category)
        {
            bool found = false;
            DisplayByCategoryRecursive(root, category, ref found);
            if (!found)
            {
                Console.WriteLine($"No items found in category: {category}");
            }
        }

        private void DisplayByCategoryRecursive(BSTNode node, string category, ref bool found)
        {
            if (node != null)
            {
                DisplayByCategoryRecursive(node.Left, category, ref found);
                if (node.Data.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    {
                    Console.WriteLine(node.Data.ToString());
                    Console.WriteLine() ;
                    found = true ;
                }
                DisplayByCategoryRecursive(node.Right, category, ref found);
            }
        }


        //==count==
        //time complexity: 0(n)

        public int Count()
        {
            return CountRecursive(root);
        }

        private int CountRecursive(BSTNode node)
        {
            if (node == null)
            {
                return 0 ;
            }
            return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }

        //==GEt all items==
        // returns all items as an array this is usefull for asaving space
        //Time complexity: 0(n)

        public MenuItem[] GetAllItems()
        {
            MenuItem[] items = new MenuItem[Count()];
            int index = 0;
            GetAllItemsRecursive(root, items, ref index);
            return items;
        }

        private void GetAllItemsRecursive(BSTNode node, MenuItem[]items,ref int index)
        {
            if (node != null)
            {
                GetAllItemsRecursive(node.Left,items,ref index);
                items[index] = node.Data ;
                index++;
                GetAllItemsRecursive(node.Right, items, ref index);
            }
        }

     }

}
