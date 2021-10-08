using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.Services;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserAPIController : Controller
    {
        // Current user service (this will be used to get user id of the currently logged in user)
        private readonly ICurrentUser _currentUserService;

        // User manager manager
        private readonly UserManager<User> userManager;

        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public UserAPIController(UserManager<User> userManager, IMapper mapper, ICurrentUser currentUserService)
        {
            // Initialize user manager with DI
            this.userManager = userManager;

            // Initialize current user service
            _currentUserService = currentUserService;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get all users
        [HttpGet("getAllUsers")]
        public async Task<JsonResult> GetAllUsers()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // The database context
            var databaseContext = new DatabaseContext();

            // Start querying the database to get list of all users
            var listOfUsers = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Include(userProfile => userProfile.RoleDetailUserProfiles)
                .ToListAsync();

            // Map list of user profile objects into list of user profile view models
            var listOfUserViewModels = _mapper.Map<List<UserProfileViewModel>>(listOfUsers);

            // Add data to response data
            responseData.Add("status", "Done");
            responseData.Add("data", listOfUserViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to get info of the currently logged in user
        [HttpGet("getCurrentUserInfo")]
        public async Task<JsonResult> GetCurrentUserInfo()
        {
            // Call the function to get user id of the currently logged in user
            int currentUserId = await _currentUserService.GetCurrentUserId();

            // The database context
            using var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var currentUserObject = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Include(userProfile => userProfile.WorkItems)
                .FirstOrDefaultAsync((userProfile) => userProfile.Id == currentUserId);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", currentUserObject);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to change password for the currently logged in user
        [HttpPatch("changePassword")]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePasswordViewModel changePasswordViewModel)
        {
            // Prepare response for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get info of the currently logged in user
            User currentUserObject = await _currentUserService.GetCurrentUserObject();

            // Start with password changing here
            var result = await userManager.ChangePasswordAsync(currentUserObject, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

            // Check to see if result of the password change is successful or not
            if (result.Succeeded)
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("data", "Password has been updated");
            } else
            {
                // Add data to the response data
                Response.StatusCode = 500;
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an issue here " + result.Errors);
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to change email for the currently logged in user
        [HttpPatch("changeEmail")]
        public async Task<JsonResult> ChangeEmail([FromBody] ChangeEmailViewModel changeEmailViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Get user object of the currently logged in user
            User currentUserObject = await _currentUserService.GetCurrentUserObject();

            // Change username and email
            currentUserObject.Email = changeEmailViewModel.NewEmail;
            currentUserObject.UserName = changeEmailViewModel.NewEmail;

            // Update the changes
            await userManager.UpdateAsync(currentUserObject);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", "Email has been updated ");

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}
