using Microsoft.AspNetCore.Mvc;

namespace TakeawayWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("UserRole", "Admin");
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Admin");
            }
            else if (username == "customer" && password == "cust123")
            {
                HttpContext.Session.SetString("UserRole", "Customer");
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Customer");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}