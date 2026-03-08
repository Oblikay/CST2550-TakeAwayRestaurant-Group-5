using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway_restaurant;

namespace TakeAwayRestaurant.Tests
{
    [TestClass]
    public class BSTTests
    {
        //   INSERT TESTS  

        [TestMethod]
        public void Insert_SingleItem_CountIsOne()
        {
            BinarySearchTree tree = new BinarySearchTree();
            MenuItem item = new MenuItem(1, "Test Item", "Starters", 5.99m, "Test description");

            tree.Insert(item);

            Assert.AreEqual(1, tree.Count());
        }

        [TestMethod]
        public void Insert_MultipleItems_CountIsCorrect()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Item A", "Mains", 8.99m, "Desc A"));
            tree.Insert(new MenuItem(5, "Item B", "Starters", 4.99m, "Desc B"));
            tree.Insert(new MenuItem(15, "Item C", "Desserts", 3.99m, "Desc C"));

            Assert.AreEqual(3, tree.Count());
        }

        [TestMethod]
        public void Insert_DuplicateId_CountStaysSame()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(1, "Original", "Mains", 5.00m, "Original desc"));
            tree.Insert(new MenuItem(1, "Updated", "Mains", 6.00m, "Updated desc"));

            Assert.AreEqual(1, tree.Count());
        }

        //   SEARCH TESTS  

        [TestMethod]
        public void SearchById_ExistingItem_ReturnsItem()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy rolls"));

            MenuItem found = tree.SearchById(10);

            Assert.IsNotNull(found);
            Assert.AreEqual("Spring Rolls", found.Name);
        }

        [TestMethod]
        public void SearchById_NonExistingItem_ReturnsNull()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy rolls"));

            MenuItem found = tree.SearchById(99);

            Assert.IsNull(found);
        }

        [TestMethod]
        public void SearchByName_ExistingItem_ReturnsItem()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy rolls"));

            MenuItem found = tree.SearchByName("Spring Rolls");

            Assert.IsNotNull(found);
            Assert.AreEqual(10, found.Id);
        }

        [TestMethod]
        public void SearchByName_CaseInsensitive_ReturnsItem()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy rolls"));

            MenuItem found = tree.SearchByName("spring rolls");

            Assert.IsNotNull(found);
        }

        //  DELETE TESTS  

        [TestMethod]
        public void Delete_ExistingItem_RemovesItem()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy rolls"));

            bool result = tree.Delete(10);

            Assert.IsTrue(result);
            Assert.AreEqual(0, tree.Count());
        }

        [TestMethod]
        public void Delete_NonExistingItem_ReturnsFalse()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy rolls"));

            bool result = tree.Delete(99);

            Assert.IsFalse(result);
            Assert.AreEqual(1, tree.Count());
        }

        [TestMethod]
        public void Delete_NodeWithTwoChildren_TreeStaysValid()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(10, "Item A", "Mains", 8.99m, "Desc"));
            tree.Insert(new MenuItem(5, "Item B", "Starters", 4.99m, "Desc"));
            tree.Insert(new MenuItem(15, "Item C", "Desserts", 3.99m, "Desc"));

            tree.Delete(10);

            Assert.AreEqual(2, tree.Count());
            Assert.IsNotNull(tree.SearchById(5));
            Assert.IsNotNull(tree.SearchById(15));
        }

        //  GET ALL ITEMS TESTS 

        [TestMethod]
        public void GetAllItems_ReturnsItemsSortedById()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(new MenuItem(20, "Item C", "Mains", 7.99m, "Desc"));
            tree.Insert(new MenuItem(5, "Item A", "Starters", 3.99m, "Desc"));
            tree.Insert(new MenuItem(10, "Item B", "Drinks", 1.99m, "Desc"));

            MenuItem[] items = tree.GetAllItems();

            Assert.AreEqual(3, items.Length);
            Assert.AreEqual(5, items[0].Id);
            Assert.AreEqual(10, items[1].Id);
            Assert.AreEqual(20, items[2].Id);
        }

        [TestMethod]
        public void EmptyTree_CountIsZero()
        {
            BinarySearchTree tree = new BinarySearchTree();

            Assert.AreEqual(0, tree.Count());
        }
    }
}