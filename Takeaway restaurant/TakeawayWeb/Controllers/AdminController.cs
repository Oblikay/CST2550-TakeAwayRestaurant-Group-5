using Microsoft.AspNetCore.Mvc;

namespace TakeawayWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly BinarySearchTree _menuTree;
        private readonly OrderBST _orderTree;

        public AdminController(BinarySearchTree menuTree, OrderBST orderTree)
        {
            _menuTree = menuTree;
            _orderTree = orderTree;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            ViewBag.MenuCount = _menuTree.Count();
            ViewBag.OrderCount = _orderTree.Count();
            return View();
        }

        public IActionResult Menu()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            ViewBag.Items = _menuTree.GetAllItems();
            return View();
        }

        public IActionResult AddItem()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(int id, string name, string category, decimal price, string description)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category))
            {
                ViewBag.Error = "Name and Category are required.";
                return View();
            }

            MenuItem item = new MenuItem(id, name, category, price, description ?? "");
            _menuTree.Insert(item);

            var db = new DatabaseHelper("restaurant.db");
            db.SaveAllMenuItems(_menuTree);

            ViewBag.Success = $"'{name}' has been added to the menu!";
            return View();
        }

        public IActionResult RemoveItem(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            bool removed = _menuTree.Delete(id);
            if (removed)
            {
                var db = new DatabaseHelper("restaurant.db");
                db.SaveAllMenuItems(_menuTree);
            }

            return RedirectToAction("Menu");
        }

        public IActionResult Orders()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            ViewBag.Orders = _orderTree.GetAllOrders();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            Order order = _orderTree.SearchById(orderId);
            if (order != null)
            {
                order.Status = status;
            }

            return RedirectToAction("Orders");
        }
    }
}