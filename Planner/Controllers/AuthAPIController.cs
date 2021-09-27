using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Planner.Models;
using Planner.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/Auth")]
    [ApiController]
    public class AuthAPIController : Controller
    {
        //[BindProperty]
        public SignUpViewModel SignUpViewModel { get; set; }

        // Login ViewModel which will be used for login operation
        //[BindProperty]
        public LoginViewModel LoginViewModel { get; set; }
        
        // User manager and sign in manager
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        // Email sender
        private readonly IEmailSender emailSender;

        // Database context
        private readonly DatabaseContext databaseContext;

        public AuthAPIController(UserManager<User> userManager,
            SignInManager<User> signInManager, IEmailSender emailSender)
        {
            // Initialize user manager and sign in manager with DI
            this.userManager = userManager;
            this.signInManager = signInManager;

            // Initialize mail service
            this.emailSender = emailSender;

            // Initialize database context
            databaseContext = new DatabaseContext();
        }

        // The function to create new user in the database
        [HttpPost("signUp")]
        public async Task<JsonResult> CreateUser()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            var viewModel = SignUpViewModel;

            if (ModelState.IsValid)
            {
                // Add data to the response data
                Response.StatusCode = 500;
                responseData.Add("status", "Uh Oh");
                responseData.Add("data", "Oops, there seems to be an error here ");

                // Return response to the client
                return new JsonResult(responseData);
            }
            //// Use this to read request body
            //var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            //// Convert JSON object into accessible object
            //var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            //// Check to see if user verified password correctly or not
            //if (SignUpViewModel.Password != SignUpViewModel.PasswordConfirm)
            //{
            //    // Add data to the response data
            //    Response.StatusCode = 400;
            //    responseData.Add("status", "Not done");
            //    responseData.Add("data", "Password and password confirm do not match");

            //    // Return response to the client
            //    return new JsonResult(responseData);
            //}

            // Create the new user profile object
            var newUserProfileObject = new UserProfile
            {
                FullName = SignUpViewModel.FullName
            };

            // Add new user profile object to the table
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
                Email = SignUpViewModel.Email,
                UserName = SignUpViewModel.Email
            };

            // Perform the sign up operation and get the result
            var result = await userManager.CreateAsync(newUser, SignUpViewModel.Password);

            if (result.Succeeded)
            {
                // Add data to the response data
                Response.StatusCode = 201;
                responseData.Add("status", "Done");
                responseData.Add("data", "User is created");
            }
            else
            {
                // Add data to the response data
                Response.StatusCode = 500;
                responseData.Add("status", "Uh Oh");
                responseData.Add("data", "Oops, there seems to be an error here " + result.Errors);
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to sign a user in
        [HttpPost("signIn")]
        public async Task<JsonResult> SignIn()
        {
            // Perform the sign in operation and get the result
            var result = await signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, true, false);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            if (result.Succeeded)
            {
                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", "You are signed in");
            }
            else
            {
                // Add data to the response data
                Response.StatusCode = 500;
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an error");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to check if user is signed in or not
        [HttpGet("checkSignInStatus")]
        public JsonResult CheckSignInStatus()
        {
            // Perform the checking operation and get the result
            var isSignedIn = signInManager.IsSignedIn(User);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            if (isSignedIn)
            {
                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", "You are signed in");
            }
            else
            {
                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", "You are not signed in");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to sign the user out
        [HttpPost("signOut")]
        public async Task<JsonResult> Logout()
        {
            // Perform the signing out operation and get the result
            await signInManager.SignOutAsync();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", "You are signed out");

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to send password reset token to user with specified email address
        [HttpPost("sendPasswordResetEmail")]
        public async Task<JsonResult> SendEmail()
        {
            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Get email of the user to get password reset token
            string userEmailToResetPassword = requestBody["userEmail"];

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Reference the database to get user object of the user who needs to get password reset token
            var userObject = await databaseContext.Users.Where((userObject) =>
                userObject.Email == userEmailToResetPassword
            ).ToListAsync();

            // If there is no account associated with that email, let the client know that as well
            if (userObject.Count == 0)
            {
                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Not found");
                responseData.Add("data", "There is no account associated with the email " + userEmailToResetPassword);

                // Return response to the client
                return new JsonResult(responseData);
            }

            // Call the function to generate password reset token for the user
            string resetToken = await userManager.GeneratePasswordResetTokenAsync(userObject[0]);

            // Send email with password reset token
            await emailSender.SendEmailAsync(userEmailToResetPassword, "Reset password", "Use this token to reset your password " + resetToken);

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", "Password reset email has been sent to " + userEmailToResetPassword);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to reset password for user with specified email address and reset password token
        [HttpPost("resetPassword")]
        public async Task<JsonResult> ResetPassword()
        {
            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Get email of the user to reset password
            string userEmailToResetPassword = requestBody["userEmail"];

            // Get password reset token of user to reset password
            string passwordResetToken = requestBody["passwordResetToken"];

            // Get new password of the user
            string newPassword = requestBody["newPassword"];

            // Get new password confirm value of the user
            string newPasswordConfirm = requestBody["newPasswordConfirm"];

            // Prepare the response data
            var responseData = new Dictionary<string, object>();

            // Check to see if user confirm the password correctly or not
            if (newPassword != newPasswordConfirm)
            {
                // Add data to the response data
                Response.StatusCode = 400;
                responseData.Add("status", "Not done");
                responseData.Add("data", "Password confirm did not match");

                // Return response to the client
                return new JsonResult(responseData);
            }

            // Reference the database to get user object of the user who needs to get password reset
            var userObject = (await databaseContext.Users.Where((userObject) =>
                userObject.Email == userEmailToResetPassword
            ).ToListAsync())[0];

            // Call the function to start with password resetting procedure
            IdentityResult passwordResetResult = await userManager.ResetPasswordAsync(userObject, passwordResetToken, newPassword);

            // If password is reset successfully, let the client know that
            if (passwordResetResult.Succeeded)
            {
                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", "Password has been reset");
            } // Otherwise, inform client of the error
            else
            {
                // Add data to the response data
                Response.StatusCode = 500;
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seems to be an error while resetting");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        [HttpDelete("deleteEveryUser")]
        public async void DeleteAll()
        {
            await databaseContext.Database.ExecuteSqlRawAsync("DELETE FROM USERS");
        }
    }
}
