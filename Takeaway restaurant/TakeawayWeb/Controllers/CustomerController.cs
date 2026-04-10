using Microsoft.AspNetCore.Mvc;

namespace TakeawayWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BinarySearchTree _menuTree;
        private readonly OrderBST _orderTree;
        private readonly DatabaseHelper _db;
        private static int _nextOrderId = 1;

        public CustomerController(BinarySearchTree menuTree, OrderBST orderTree, DatabaseHelper db)
        {
            _menuTree = menuTree;
            _orderTree = orderTree;
            _db = db;

            Order[] existing = _orderTree.GetAllOrders();
            for (int i = 0; i < existing.Length; i++)
            {
                if (existing[i].OrderId >= _nextOrderId)
                {
                    _nextOrderId = existing[i].OrderId + 1;
                }
            }
        }

        private bool IsCustomer()
        {
            return HttpContext.Session.GetString("UserRole") == "Customer";
        }

        public IActionResult Index()
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");
            ViewBag.Items = _menuTree.GetAllItems();
            ViewBag.OrderCount = _orderTree.Count();

            return View();
        }

        public IActionResult Search(string query, string searchType)
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");

            ViewBag.Query = query;
            ViewBag.SearchType = searchType;

            if (!string.IsNullOrWhiteSpace(query))
            {
                if (searchType == "id" && int.TryParse(query, out int id))
                {
                    MenuItem found = _menuTree.SearchById(id);
                    if (found != null)
                    {
                        ViewBag.Results = new MenuItem[] { found };
                    }
                    else
                    {
                        ViewBag.Results = new MenuItem[0];
                    }
                }
                else if (searchType == "name")
                {
                    MenuItem found = _menuTree.SearchByName(query);
                    if (found != null)
                    {
                        ViewBag.Results = new MenuItem[] { found };
                    }
                    else
                    {
                        ViewBag.Results = new MenuItem[0];
                    }
                }
            }

            return View();
        }

        public IActionResult Menu(string category)
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");

            ViewBag.AllItems = _menuTree.GetAllItems();
            ViewBag.SelectedCategory = category;

            if (!string.IsNullOrWhiteSpace(category) && category != "All")
            {
                MenuItem[] allItems = _menuTree.GetAllItems();
                int count = 0;
                for (int i = 0; i < allItems.Length; i++)
                {
                    if (allItems[i].Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                        count++;
                }
                MenuItem[] filtered = new MenuItem[count];
                int idx = 0;
                for (int i = 0; i < allItems.Length; i++)
                {
                    if (allItems[i].Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    {
                        filtered[idx] = allItems[i];
                        idx++;
                    }
                }
                ViewBag.Items = filtered;
            }
            else
            {
                ViewBag.Items = _menuTree.GetAllItems();
            }

            return View();
        }

        public IActionResult Order()
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");
            ViewBag.Items = _menuTree.GetAllItems();
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(string customerName, string customerPhone, string customerAddress, string selectedItems)
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(selectedItems))
            {
                ViewBag.Error = "Please fill in your details and select at least one item.";
                ViewBag.Items = _menuTree.GetAllItems();
                return View("Order");
            }

            Order order = new Order(_nextOrderId, customerName, customerPhone ?? "", customerAddress ?? "");

            string[] itemIds = selectedItems.Split(',');
            for (int i = 0; i < itemIds.Length; i++)
            {
                if (int.TryParse(itemIds[i].Trim(), out int itemId))
                {
                    MenuItem item = _menuTree.SearchById(itemId);
                    if (item != null)
                    {
                        order.AddItem(item);
                    }
                }
            }

            if (order.ItemCount > 0)
            {
                _orderTree.Insert(order);
                _db.SaveOrder(order);
                _nextOrderId++;
                ViewBag.Success = $"Order #{order.OrderId} placed successfully! Total: £{order.TotalPrice:F2}";
                ViewBag.Order = order;
                return View("OrderConfirmation");
            }

            ViewBag.Error = "No valid items selected.";
            ViewBag.Items = _menuTree.GetAllItems();
            return View("Order");
        }

        public IActionResult MyOrders()
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");
            ViewBag.Orders = _orderTree.GetAllOrders();
            return View();
        }

        public IActionResult AI()
        {
            if (!IsCustomer()) return RedirectToAction("Login", "Account");
            return View();
        }
    }
}