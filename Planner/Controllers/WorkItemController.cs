using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("dashboard")]
    public class WorkItemController : Controller
    {
        [HttpGet("main")]
        public IActionResult Index()
        {
            ViewData["Header"] = "Hello there!";
            return View();
        }
    }
}
