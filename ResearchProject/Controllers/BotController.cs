using Microsoft.AspNetCore.Mvc;

namespace ResearchProject.Controllers
{
    public class BotController : Controller
    {
        public IActionResult WebChat()
        {
            return View();
        }
    }
}
