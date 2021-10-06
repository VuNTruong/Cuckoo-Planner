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
using Planner.Services;
using AutoMapper;

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

        // Email sender
        private readonly IEmailSender emailSender;

        // Database context
        private readonly DatabaseContext databaseContext;

        // Http Utils
        private IHttpUtils _httpUtils;

        // Error getter
        private IErrorGetter _errorGetter;

        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public AuthAPIController(UserManager<User> userManager,
            SignInManager<User> signInManager, IEmailSender emailSender, IHttpUtils httpUtils, IErrorGetter errorGetter, IMapper mapper)
        {
            // Initialize user manager and sign in manager with DI
            this.userManager = userManager;
            this.signInManager = signInManager;

            // Initialize mail service
            this.emailSender = emailSender;

            // Initialize database context
            databaseContext = new DatabaseContext();

            // Initialize Http utils
            _httpUtils = httpUtils;

            // Initialize error getter
            _errorGetter = errorGetter;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to create new user in the database
        [HttpPost("signUp")]
        public async Task<JsonResult> CreateUser([FromBody] SignUpViewModel signUpViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();
            
            // Create the new user profile object
            var newUserProfileObject = new UserProfile
            {
                FullName = signUpViewModel.FullName
            };

            // Add new user profile object to the table
            await databaseContext.UserProfiles
                .AddAsync(newUserProfileObject);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Get user profile id of the created user profile
            int createdUserProfileId = newUserProfileObject.Id;

            // Create the new user object
            var newUser = _mapper.Map<User>(signUpViewModel);
            newUser.UserProfileId = createdUserProfileId;

            // Perform the sign up operation and get the result
            var result = await userManager.CreateAsync(newUser, signUpViewModel.Password);

            if (result.Succeeded)
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("data", "User is created");
            }
            else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an error");
            }

            return new JsonResult(responseData);
        }

        // The function to sign a user in
        [HttpPost("signIn")]
        public async Task<JsonResult> SignIn([FromBody] LoginViewModel loginViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Perform the sign in operation and get the result
            var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);

            if (result.Succeeded)
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("data", "You are signed in");
            }
            else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an error while signing you in");
            }

            return new JsonResult(responseData);
        }

        // The function to check if user is signed in or not
        [HttpGet("checkSignInStatus")]
        public JsonResult CheckSignInStatus()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Perform the checking operation and get the result
            var isSignedIn = signInManager.IsSignedIn(User);

            if (isSignedIn)
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("data", "You are signed in");
            }
            else
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("data", "You are not signed in");
            }

            return new JsonResult(responseData);
        }

        // The function to sign the user out
        [HttpPost("signOut")]
        public async Task<JsonResult> Logout()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Perform the signing out operation and get the result
            await signInManager.SignOutAsync();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", "You are signed out");

            return new JsonResult(responseData);
        }

        // The function to send password reset token to user with specified email address
        [HttpPost("sendPasswordResetEmail")]
        public async Task<JsonResult> SendEmail(ResetPasswordViewModel resetPasswordViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Reference the database to get user object of the user who needs to get password reset token
            var userObject = await databaseContext.Users.Where((userObject) =>
                userObject.Email == resetPasswordViewModel.Email
            ).ToListAsync();

            // If there is no account associated with that email, let the client know that as well
            if (userObject.Count == 0)
            {
                // Add data to the response data
                responseData.Add("status", "Not found");
                responseData.Add("data", $"There is no account associated with the email {resetPasswordViewModel.Email}");

                return new JsonResult(responseData);
            }

            // Call the function to generate password reset token for the user
            string resetToken = await userManager.GeneratePasswordResetTokenAsync(userObject[0]);

            // Send email with password reset token
            await emailSender.SendEmailAsync(resetPasswordViewModel.Email, "Reset password", $"Use this token to reset your password {resetToken}");

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", $"An email has been sent to {resetPasswordViewModel.Email}");

            return new JsonResult(responseData);
        }

        // The function to reset password for user with specified email address and reset password token
        [HttpPost("resetPassword")]
        public async Task<JsonResult> ResetPassword()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

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
                responseData.Add("status", "Done");
                responseData.Add("data", $"Password has been reset for {userEmailToResetPassword}");
            } // Otherwise, inform client of the error
            else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an error");
            }

            return new JsonResult(responseData);
        }

        // Only use this one if you screw things up so bad
        // If it's still fixable, DON'T TOUCH IT
        [HttpDelete("deleteEveryUser")]
        public async void DeleteAll()
        {
            await databaseContext.Database.ExecuteSqlRawAsync("DELETE FROM USERS");
        }
    }
}
