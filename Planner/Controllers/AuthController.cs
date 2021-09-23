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

        // The view where user can request for password reset token
        [HttpGet("resetpassword")]
        public IActionResult ForgotPassword()
        {
            ViewData["Header"] = "Reset password";
            return View();
        }

        // The view where user can enter password reset token and create new password
        [HttpGet("createnewpassword")]
        public IActionResult ResetPassword(string userEmail)
        {
            // Pass email address of the user that need to reset email to the view
            ViewData["UserEmail"] = userEmail;

            // Header of the view
            ViewData["Header"] = "Create new password";
            return View();
        }

        // The view where user is informed that entered email for password reset is not correct
        [HttpGet("incorrectemailresetpassword")]
        public IActionResult ResetPasswordEmailNotFound()
        {
            ViewData["Header"] = "Email not found ";
            return View();
        }

        // The view where user is informed that password has been reset
        [HttpGet("passwordresetdone")]
        public IActionResult ResetPasswordDone()
        {
            ViewData["Header"] = "Done";
            return View();
        }

        // The view where user is informed that account has been created
        [HttpGet("signupdone")]
        public IActionResult SignUpDone()
        {
            ViewData["Header"] = "Done";
            return View();
        } 

        public AuthController()
        { }
    }
}
