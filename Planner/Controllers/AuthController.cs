using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.Utils;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        // User manager and sign in manager
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        // Email sender
        private readonly IEmailSender emailSender;

        // Database context
        private readonly DatabaseContext databaseContext;

        // Validation error getter
        private ValidationErrorGetter validationErrorGetter;

        // Identity error getter
        private IdentityErrorGetter identityErrorGetter;

        // Constructor
        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager, IEmailSender emailSender)
        {
            // Initialize user manager and sign in manager with DI
            this.userManager = userManager;
            this.signInManager = signInManager;

            // Initialize email sender
            this.emailSender = emailSender;

            // Initialize database context
            databaseContext = new DatabaseContext();
        }

        // The function to view login page
        [HttpGet("login")]
        public IActionResult SignIn()
        {
            ViewData["Header"] = "Welcome back!";
            return View();
        }

        // The function to perform login operation
        [HttpPost("SignInAction")]
        public async Task<ActionResult> SignInAction(LoginViewModel loginViewModel)
        {
            // Initialize list of errors
            loginViewModel.LoginValidationErrors = new List<string>();

            // Perform model validation
            if (!ModelState.IsValid)
            {
                // Initialize validation error getter
                validationErrorGetter = new ValidationErrorGetter
                {
                    ModelState = ViewData.ModelState
                };

                // Get errors
                loginViewModel.LoginValidationErrors = validationErrorGetter.ValidationErrorsGenerator();

                // Return the view
                ViewData["Header"] = "Welcome back";
                return View("SignIn", loginViewModel);
            }

            // Perform the sign in operation and get the result
            var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);

            if (result.Succeeded)
            {
                // Redirect user to the main work item overview
                return RedirectToAction(actionName: "WorkItemOverview", controllerName: "WorkItem");
            }
            else
            {
                // Initialize identity error getter
                identityErrorGetter = new IdentityErrorGetter
                {
                    UserManager = userManager,
                    SignInResult = result
                };

                // Get errors
                loginViewModel.LoginValidationErrors = await identityErrorGetter.LoginErrorGenerator(loginViewModel.Email);

                // Return the view
                ViewData["Header"] = "Welcome back";
                return View("SignIn", loginViewModel);
            }
        }

        // The function to show signup page
        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            ViewData["Header"] = "Welcome!";
            return View();
        }

        // The function to perform sign up operation
        [HttpPost("SignUpAction")]
        public async Task<ActionResult> SignUpAction(SignUpViewModel signUpViewModel)
        {
            // Check to see if model is valid or not
            if (!ModelState.IsValid)
            {
                // Initialize list of errors
                signUpViewModel.ValidationErrors = new List<string>();

                // Initialize validation error getter
                validationErrorGetter = new ValidationErrorGetter
                {
                    ModelState = ViewData.ModelState
                };

                // Get errors
                signUpViewModel.ValidationErrors = validationErrorGetter.ValidationErrorsGenerator();

                // Return the view
                ViewData["Header"] = "Welcome";
                return View("SignUp", signUpViewModel);
            }

            // Create the new user profile object
            var newUserProfileObject = new UserProfile
            {
                FullName = signUpViewModel.FullName
            };

            // Add new user profile object to the user profile table
            await databaseContext.UserProfiles
                .AddAsync(newUserProfileObject);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Get user profile id of the created user profile
            int createdUserProfileId = newUserProfileObject.Id;

            // Create the new user object
            var newUser = new User
            {
                UserProfileId = createdUserProfileId,
                Email = signUpViewModel.Email,
                UserName = signUpViewModel.Email
            };

            // Perform the sign up operation and get the result
            var result = await userManager.CreateAsync(newUser, signUpViewModel.Password);

            if (result.Succeeded)
            {
                // Redirect user to the sign up done page
                return RedirectToAction(actionName: "signupdone", controllerName: "Auth");
            } else
            {
                // Return the view
                return View("SignUp");
            }
        }

        // The view where user can request for password reset token
        [HttpGet("resetpassword")]
        public IActionResult ForgotPassword()
        {
            ViewData["Header"] = "Reset password";
            return View();
        }

        // The function to perform password reset operation
        [HttpPost("PasswordResetAction")]
        public async Task<ActionResult> PasswordResetAction(ResetPasswordViewModel resetPasswordViewModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // Initialize list of errors
                resetPasswordViewModel.ValidationErrors = new List<string>();

                // Initialize validation error getter
                validationErrorGetter = new ValidationErrorGetter
                {
                    ModelState = ViewData.ModelState
                };

                // Get errors
                resetPasswordViewModel.ValidationErrors = validationErrorGetter.ValidationErrorsGenerator();

                ViewData["Header"] = "Reset password";
                return View("ForgotPassword", resetPasswordViewModel);
            }

            // Reference the database to get user object of the user who needs to get password reset token
            var userObject = await databaseContext.Users.Where(userObject =>
                userObject.Email == resetPasswordViewModel.Email
            ).ToListAsync();
            
            // If there is no account associated with that email, let the user know it
            if (userObject.Count == 0)
            {
                return RedirectToAction(actionName: "ResetPasswordEmailNotFound", controllerName: "Auth");
            }

            // Call the function to generate password reset token for the user
            string resetToken = await userManager.GeneratePasswordResetTokenAsync(userObject[0]);

            // Send email with password reset token
            await emailSender.SendEmailAsync(resetPasswordViewModel.Email, "Reset password", "Use this token to reset your password " + resetToken);

            // Up to this point, password reset has been sent and we will take user to the next step
            return RedirectToAction(actionName: "ResetPassword", controllerName: "Auth", new { userEmail = resetPasswordViewModel.Email });
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

        // The function to perform create new password operation
        [HttpPost("CreateNewPasswordAction")]
        public async Task<ActionResult> CreateNewPasswordAction (CreateNewPasswordViewModel createNewPasswordViewModel)
        {
            // Initialize list of errors
            createNewPasswordViewModel.ValidationErrors = new List<string>();

            // Validate the model
            if (!ModelState.IsValid)
            {
                // Initialize validation error getter
                validationErrorGetter = new ValidationErrorGetter
                {
                    ModelState = ViewData.ModelState
                };

                // Get errors
                createNewPasswordViewModel.ValidationErrors = validationErrorGetter.ValidationErrorsGenerator();

                ViewData["Header"] = "Create new password;";
                return View("ResetPassword", createNewPasswordViewModel);
            }

            // Reference the database to get user object of the user who needs to reset password
            var userObject = (await databaseContext.Users.Where((userObject) =>
                userObject.Email == createNewPasswordViewModel.Email
            ).ToListAsync())[0];

            // Call the function to start resetting password
            IdentityResult passwordResetResult = await userManager.ResetPasswordAsync(userObject, createNewPasswordViewModel.PasswordResetToken, createNewPasswordViewModel.NewPassword);
            
            // Check to see if password reset operation is successful or not
            if (passwordResetResult.Succeeded)
            {
                return RedirectToAction(actionName: "ResetPasswordDone", controllerName: "Auth");
            } else
            {
                return View("createnewpassword");
            }
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

        // The function which show the user that account has been created
        [HttpGet("signupdone")]
        public IActionResult SignUpDone()
        {
            ViewData["Header"] = "Done";
            return View();
        }

        // The function which take user to the login page
        [HttpGet("SignUpDoneSignInAction")]
        public ActionResult SignUpDoneSignInAction()
        {
            // Redirect user to the login page
            return RedirectToAction(actionName: "SignIn", controllerName: "Auth");
        }
    }
}
