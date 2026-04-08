using Microsoft.AspNetCore.Mvc;

namespace TakeawayWeb.Controllers
{
    public class AIController : Controller
    {
        private readonly BinarySearchTree _menuTree;
        private readonly OrderBST _orderTree;

        public AIController(BinarySearchTree menuTree, OrderBST orderTree)
        {
            _menuTree = menuTree;
            _orderTree = orderTree;
        }

        [HttpPost]
        public IActionResult Chat([FromBody] ChatRequest request)
        {
            if (HttpContext.Session.GetString("UserRole") == null)
                return Unauthorized();

            AIAgent agent = new AIAgent(_menuTree, _orderTree);
            string response = agent.GetResponse(request.Message);

            return Json(new { reply = response });
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; } = "";
    }
}