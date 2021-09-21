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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/Auth")]
    [ApiController]
    public class AuthAPIController : Controller
    {
        // User manager and sign in manager
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;

        public AuthAPIController(UserManager<User> userManager,
            SignInManager<User> signInManager, IEmailSender emailSender)
        {
            // Initialize user manager and sign in manager with DI
            this.userManager = userManager;
            this.signInManager = signInManager;

            // Initialize mail service
            this.emailSender = emailSender;
        }

        // The function to create new user in the database
        [HttpPost("signUp")]
        public async Task<JsonResult> createUser()
        {
            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Check to see if user verified password correctly or not
            if (requestBody["password"] != requestBody["passwordConfirm"])
            {
                // Add data to the response data
                Response.StatusCode = 400;
                responseData.Add("status", "Not done");
                responseData.Add("data", "Password and password confirm do not match");

                // Return response to the client
                return new JsonResult(responseData);
            }

            // Create the new user object
            var newUser = new User
            {
                fullName = requestBody["fullName"],
                Email = requestBody["email"],
                UserName = requestBody["email"]
            };

            // Perform the sign up operation and get the result
            var result = await userManager.CreateAsync(newUser, requestBody["password"]);

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
        public async Task<JsonResult> signIn()
        {
            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Perform the sign in operation and get the result
            var result = await signInManager.PasswordSignInAsync(requestBody["email"], requestBody["password"], true, false);

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
        public JsonResult checkSignInStatus()
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
        public async Task<JsonResult> signOut()
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

        // The function to get info of the currently logged in user
        [HttpGet("getInfoOfCurrentUser")]
        public async Task<JsonResult> getInfoOfCurrentUser()
        {
            // Get user id of the currently logged in user
            string currentUserId = userManager.GetUserId(HttpContext.User);

            // Create the database context object
            var databaseContext = new DatabaseContext();

            // Reference the database to get user object of the currently logged in user
            var userObject = (await databaseContext.Users.Where((userObject) =>
                userObject.Id == currentUserId
            ).ToListAsync())[0];

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", userObject);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to send password reset token to user with specified email address
        [HttpPost("sendPasswordResetEmail")]
        public async Task<JsonResult> sendEmail()
        {
            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Get email of the user to get password reset token
            string userEmailToResetPassword = requestBody["userEmail"];

            // Create the database context object
            var databaseContext = new DatabaseContext();

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
        public async Task<JsonResult> resetPassword()
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

            // Create the database context object
            var databaseContext = new DatabaseContext();

            // Reference the database to get user object of the user who needs to get password reset
            var userObject = (await databaseContext.Users.Where((userObject) =>
                userObject.Email == userEmailToResetPassword
            ).ToListAsync())[0];

            // Call the function to start with password resetting procedure
            IdentityResult passwordResetResult = await userManager.ResetPasswordAsync(userObject, passwordResetToken, newPassword);

            // Prepare the response data
            var responseData = new Dictionary<string, object>();

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
    }
}
