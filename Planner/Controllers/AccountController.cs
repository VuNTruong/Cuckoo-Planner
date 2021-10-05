using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Planner.ViewModels;
using System.Security.Claims;
using Planner.Services;
using Microsoft.AspNetCore.Identity;
using Planner.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    //[Route("Account")]
    public class AccountController : Controller
    {
        // Error getter
        private IErrorGetter _errorGetter;

        // Sign in manager
        private readonly SignInManager<User> _signInManager;

        public AccountController (IErrorGetter errorGetter, SignInManager<User> signInManager)
        {
            // Initialize Error getter
            _errorGetter = errorGetter;

            // Initialize sign in manager
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("LoginAction")]
        public async Task<IActionResult> LoginAction(LoginViewModel loginViewModel, string returnUrl = null)
        {
            // Initialize list of errors
            loginViewModel.LoginValidationErrors = new List<string>();

            // Perform model validation
            if (!ModelState.IsValid)
            {
                // Get errors
                loginViewModel.LoginValidationErrors = _errorGetter.ValidationErrorsGenerator(ViewData.ModelState);

                // Return the view
                ViewData["Header"] = "Welcome back";
                return View("SignIn", loginViewModel);
            }

            // Perform the sign in operation and get the result
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);

            if (result.Succeeded)
            {
                // Call the function to and go to the return URL
                return redirectToLocal(returnUrl);
            } else
            {
                // Return the view
                ViewData["Header"] = "Welcome back";
                return View("SignIn", loginViewModel);
            }
        }

        private IActionResult redirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(actionName: "WorkItemOverview", controllerName: "WorkItem");
        }

        // The function to show the view which will let the user know that user is not authorized
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
