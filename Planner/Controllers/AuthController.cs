using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        // The view where user can login
        [HttpGet("login")]
        public IActionResult SignIn()
        {
            ViewData["Header"] = "Welcome back!";
            return View();
        }

        // The view where user can sign up
        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            ViewData["Header"] = "Welcome!";
            return View();
        }

        // The view where user can reset password
        [HttpGet("resetpassword")]
        public IActionResult ForgotPassword()
        {
            ViewData["Header"] = "Reset password";
            return View();
        }

        public AuthController()
        { }
    }
}
