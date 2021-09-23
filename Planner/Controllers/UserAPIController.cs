using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Planner.Models;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserAPIController : Controller
    {
        // User manager manager
        private readonly UserManager<User> UserManager;

        // Constructor
        public UserAPIController(UserManager<User> UserManager)
        {
            // Initialize user manager with DI
            this.UserManager = UserManager;
        }

        // The function to get all users
        [HttpGet("getAllUsers")]
        public async Task<JsonResult> GetAllUsers()
        {
            // The database context
            var databaseContext = new DatabaseContext();

            // Start querying the database to get list of all users
            // also include all work items and user identity object
            // associated with the user
            var listOfUsers = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.WorkItems)
                .Include(userProfile => userProfile.User).ToListAsync();

            // Prepare response data for the client
            var ResponseData = new Dictionary<string, object>();

            // Add data to response data
            Response.StatusCode = 200;
            ResponseData.Add("status", "Done");
            ResponseData.Add("data", listOfUsers);

            // Return response to the client
            return new JsonResult(ResponseData);
        }

        // The function to get info of the currently logged in user
        [HttpGet("getCurrentUserInfo")]
        public async Task<JsonResult> GetCurrentUserInfo()
        {
            // Get user id of the currently logged in user
            string currentUserId = UserManager.GetUserId(HttpContext.User);

            // The database context
            using var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var currentUserObject = (await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Include(userProfile => userProfile.WorkItems)
                .Where((userProfile) => userProfile.User.Id == currentUserId)
                .ToListAsync())[0];

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", currentUserObject);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to change password for the currently logged in user
        [HttpPatch("changePassword")]
        public async Task<JsonResult> ChangePassword()
        {
            // Read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into Dictionary object
            var requestBody = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Prepare response for the client
            var responseData = new Dictionary<string, object>();

            // Get user id of the currently logged in user
            string currentUserId = UserManager.GetUserId(HttpContext.User);

            // Find the user object of the currently logged in user
            var currentUserObject = await UserManager.FindByIdAsync(currentUserId);

            // Start with password changing here
            var result = await UserManager.ChangePasswordAsync(currentUserObject, requestBody["currentPassword"], requestBody["newPassword"]);

            // Check to see if result of the password change is successful or not
            if (result.Succeeded)
            {
                // Add data to the response data
                Response.StatusCode = 200;
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
        public async Task<JsonResult> ChangeEmail()
        {
            // Read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object (Dict)
            var requestBody = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Get user id of the currently logged in user
            string currentUserId = UserManager.GetUserId(HttpContext.User);

            // Find the user object of the currently logged in user
            var currentUserObject = await UserManager.FindByIdAsync(currentUserId);

            // Change username and email
            currentUserObject.Email = requestBody["newEmail"];
            currentUserObject.UserName = requestBody["newEmail"];

            // Update the changes
            await UserManager.UpdateAsync(currentUserObject);

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", "Email has been updated ");

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}
