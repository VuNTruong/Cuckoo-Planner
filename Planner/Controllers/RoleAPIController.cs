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
    [Authorize(Roles = "Admin")]
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
        public async Task<JsonResult> CreateNewRole ([FromBody] CreateRoleViewModel createRoleViewModel)
        {
            // Response data for the client
            List<string> responseBodyKeys;
            List<object> responseBodyData;

            // The database context
            DatabaseContext databaseContext = new DatabaseContext();

            // Check and see if role is already exist or not
            var roleExists = await _roleManager.RoleExistsAsync(createRoleViewModel.NewRoleName);

            // Create role result
            IdentityResult roleCreateResult;

            if (!roleExists)
            {
                // Create the new role detail object
                var newRoleDetailObject = new RoleDetail
                {
                    RoleDescription = createRoleViewModel.NewRoleDescription
                };

                // Add new role detail object to the role detail table
                await databaseContext.RoleDetails
                    .AddAsync(newRoleDetailObject);

                // Save changes
                await databaseContext.SaveChangesAsync();

                // Get role detail id of the created role detail
                int createdRoleDetailId = newRoleDetailObject.Id;

                // Create the new role object
                Role newRole = new Role
                {
                    Name = createRoleViewModel.NewRoleName,
                    RoleDetailId = createdRoleDetailId
                };

                // Create new role and seed them into the database
                roleCreateResult = await _roleManager.CreateAsync(newRole);

                if (roleCreateResult.Succeeded)
                {
                    // Add data to the response data
                    responseBodyKeys = new List<string> { "status", "data" };
                    responseBodyData = new List<object> { "Done. Role is created", newRole };

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
                .Where((userProfile) => userProfile.Id == addRoleToUserViewModel.UserId).ToListAsync())[0].User;

            // Reference the database to get role object of the role that will be assigned to the user
            var roleObject = (await databaseContext.RoleDetails
                .Include(roleDetail => roleDetail.Role)
                .Where(roleDetail => roleDetail.Id == addRoleToUserViewModel.RoleId).ToListAsync())[0].Role;

            // Create new record in the role detail user profile table to assign role to the user
            await databaseContext.RoleDetailUserProfiles
                .AddAsync(new RoleDetailUserProfile
                {
                    RoleDetailId = addRoleToUserViewModel.RoleId,
                    UserProfileId = addRoleToUserViewModel.UserId
                });

            // Assign role to a user
            var result = await _userManager.AddToRoleAsync(userObject, roleObject.Name);

            if (result.Succeeded)
            {
                // Add data to the response data
                responseBodyKeys = new List<string> { "status", "user", "role" };
                responseBodyData = new List<object> { "Done. Role is assigned", userObject, roleObject };

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

        // The function to get all roles in the system
        [HttpGet("getAllRoles")]
        public JsonResult GetAllRoles ()
        {
            // Response data for the client
            List<string> responseBodyKeys;
            List<object> responseBodyData;

            // Read all roles into a list of strings
            List<string> roles = _roleManager.Roles.Select(role => role.Name).ToList();

            // The database context
            DatabaseContext databaseContext = new DatabaseContext();

            var data = databaseContext.RoleDetailUserProfiles
                .Include(roleDetailUserProfile => roleDetailUserProfile.UserProfile)
                .Include(roleDetailUserProfile => roleDetailUserProfile.RoleDetail)
                .ThenInclude(roleDetail => roleDetail.Role);

            // Add data to the response data
            responseBodyKeys = new List<string> { "status", "data" };
            responseBodyData = new List<object> { "Done", data };

            return _httpUtils.GetResponseData(200, responseBodyKeys, responseBodyData, Response);
        }

        // The function to remove a role
        // After calling this function, user assigned to a role which is going to be removed
        // will lose that role as well
        [HttpDelete("deleteRole")]
        public async Task<JsonResult> DeleteRole ([FromBody] DeleteRoleViewModel deleteRoleViewModel)
        {
            // Response data for the client
            List<string> responseBodyKeys;
            List<object> responseBodyData;

            // Database context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get role detail user profile entity that contains role which going to be removed
            var roleDetailUserProfileData = (await databaseContext.RoleDetailUserProfiles
                .Where(roleDetailUserProfile => roleDetailUserProfile.RoleDetail.Id == deleteRoleViewModel.RoleDetailId)
                .Include(roleDetailUserProfile => roleDetailUserProfile.RoleDetail).ThenInclude(roleDetail => roleDetail.Role)
                .Include(roleDetailUserProfile => roleDetailUserProfile.UserProfile).ThenInclude(userProfile => userProfile.User)
                .ToListAsync())[0];

            // Reference the database to get user role entity that containd role which is going to be removed
            var userRoleData = (await databaseContext.UserRoles
                .Where(userRole => userRole.RoleId == roleDetailUserProfileData.RoleDetail.Role.Id)
                .Where(userRole => userRole.UserId == roleDetailUserProfileData.UserProfile.User.Id)
                .ToListAsync())[0];

            // Reference the database to get role detail entity that is associated with the role which is going to be removed
            // this includes role object which is going to be removed
            var roleDetailData = (await databaseContext.RoleDetails
                .Where(roleDetail => roleDetail.Id == deleteRoleViewModel.RoleDetailId)
                .Include(roleDetail => roleDetail.Role)
                .ToListAsync())[0];

            // Remove the user role
            databaseContext.Remove(userRoleData);

            // Remove the role detail object which will remove role object as well
            databaseContext.Remove(roleDetailData);

            // Remove the role detail user profile data
            databaseContext.Remove(roleDetailUserProfileData);

            // Apply changes to the database
            await databaseContext.SaveChangesAsync();

            // Add data to the response data
            responseBodyKeys = new List<string> { "status", "data" };
            responseBodyData = new List<object> { "Done", "Role is removed" };

            return _httpUtils.GetResponseData(200, responseBodyKeys, responseBodyData, Response);
        }
    }
}
