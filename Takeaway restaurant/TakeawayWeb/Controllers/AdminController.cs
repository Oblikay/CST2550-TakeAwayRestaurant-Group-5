using Microsoft.AspNetCore.Mvc;

namespace TakeawayWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly BinarySearchTree _menuTree;
        private readonly OrderBST _orderTree;
        private readonly DatabaseHelper _db;


        public AdminController(BinarySearchTree menuTree, OrderBST orderTree, DatabaseHelper db)
        {
            _menuTree = menuTree;
            _orderTree = orderTree;
            _db = db;

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
            Order[] allOrders = _orderTree.GetAllOrders();
            int pendingCount = 0;
            for (int i = 0; i < allOrders.Length; i++)
            {
                if (allOrders[i].Status == "Pending") pendingCount++;
            }
            ViewBag.PendingCount = pendingCount;
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

            _db.SaveAllMenuItems(_menuTree);
 
            ViewBag.Success = $"'{name}' has been added to the menu!";
            return View();
        }

        public IActionResult RemoveItem(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            bool removed = _menuTree.Delete(id);
            if (removed)
            {
                _db.SaveAllMenuItems(_menuTree);
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
                _db.SaveAllMenuItems(_menuTree);
                _db.UpdateOrderStatus(orderId, status);
            }

            return RedirectToAction("Orders");
        }
    }
}