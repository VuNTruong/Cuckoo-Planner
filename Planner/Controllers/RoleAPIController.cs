using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/Role")]
    [ApiController]
    public class RoleAPIController : Controller
    {
        // User manager and role manager
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Http Utils
        private readonly IHttpUtils _httpUtils;

        public RoleAPIController (UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpUtils httpUtils)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            // Initialize http utils
            _httpUtils = httpUtils;
        }

        // The function to create new role
        [HttpPost("createNewRole")]
        //[Authorize(Roles="Admin")]
        public async Task<JsonResult> CreateNewRole ([FromBody] CreateRoleViewModel createRoleViewModel)
        {
            // Response data for the client
            List<string> responseBodyKeys;
            List<object> responseBodyData;

            // Check and see if role is already exist or not
            var roleExists = await _roleManager.RoleExistsAsync(createRoleViewModel.NewRoleName);

            // Create role result
            IdentityResult roleCreateResult;

            if (!roleExists)
            {
                // Create the new role object
                IdentityRole newRole = new IdentityRole(createRoleViewModel.NewRoleName);

                // Create new role and seed them into the database
                roleCreateResult = await _roleManager.CreateAsync(newRole);

                if (roleCreateResult.Succeeded)
                {
                    // Add data to the response data
                    responseBodyKeys = new List<string> { "status", "data" };
                    responseBodyData = new List<object> { "Done", "Role is created" };

                    return _httpUtils.GetResponseData(200, responseBodyKeys, responseBodyData, Response);
                } else
                {
                    // Add data to the response data
                    responseBodyKeys = new List<string> { "status", "data" };
                    responseBodyData = new List<object> { "Not done", "Role is not created" };

                    return _httpUtils.GetResponseData(500, responseBodyKeys, responseBodyData, Response);
                }
            } else
            {
                // Add data to the response data
                responseBodyKeys = new List<string> { "status", "data" };
                responseBodyData = new List<object> { "Not done", "Role is already exists" };

                return _httpUtils.GetResponseData(400, responseBodyKeys, responseBodyData, Response);
            }
        }

        // The function to add role to a user with specified user id
        [Authorize]
        [HttpPost("addRoleToAUser")]
        public async Task<JsonResult> AddRoleToUser ([FromBody] AddRoleToUserViewModel addRoleToUserViewModel)
        {
            // Response data for the client
            List<string> responseBodyKeys;
            List<object> responseBodyData;

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get user object of the user to assign role to
            var userObject = (await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Include(userProfile => userProfile.WorkItems)
                .Where((userProfile) => userProfile.Id == 2).ToListAsync())[0].User;

            //var roleName = await _userManager.GetRolesAsync(userObject);

            // Assign role to a user
            var result = await _userManager.AddToRoleAsync(userObject, addRoleToUserViewModel.RoleName);

            if (result.Succeeded)
            {
                // Add data to the response data
                responseBodyKeys = new List<string> { "status", "data" };
                responseBodyData = new List<object> { "Done", "Role is assigned" };

                return _httpUtils.GetResponseData(200, responseBodyKeys, responseBodyData, Response);
            }
            else
            {
                // Add data to the response data
                responseBodyKeys = new List<string> { "status", "data" };
                responseBodyData = new List<object> { "Not done", "There's an error while assigning role" };

                return _httpUtils.GetResponseData(500, responseBodyKeys, responseBodyData, Response);
            }
        }
    }
}
