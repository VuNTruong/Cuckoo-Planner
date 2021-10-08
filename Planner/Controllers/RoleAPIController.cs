using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
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

        // Auto mapper
        private readonly IMapper _mapper;

        public RoleAPIController (UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to create new role
        [HttpPost("createNewRole")]
        public async Task<JsonResult> CreateNewRole ([FromBody] CreateRoleViewModel createRoleViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

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

                // Map the newly created role object into the role view model
                RoleViewModel roleViewModel = _mapper.Map<RoleViewModel>(newRole);

                if (roleCreateResult.Succeeded)
                {
                    // Add data to the response data
                    responseData.Add("status", "Done. Role is created");
                    responseData.Add("data", roleViewModel);
                } else
                {
                    // Add data to the response data
                    responseData.Add("status", "Not done");
                    responseData.Add("data", "Role was not created");
                }
            } else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "Role has already existed");
            }

            return new JsonResult(responseData);
        }

        // The function to add role to a user with specified user id
        [HttpPost("addRoleToAUser")]
        public async Task<JsonResult> AddRoleToUser ([FromBody] RoleUserViewModel addRoleToUserViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get user object of the user to assign role to
            var userObject = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.Id == addRoleToUserViewModel.UserId);

            // Reference the database to get role object of the role that will be assigned to the user
            // Also map it to the role view model
            var roleObject = _mapper.Map<RoleViewModel>((await databaseContext.RoleDetails
                .Include(roleDetail => roleDetail.Role)
                .FirstOrDefaultAsync(roleDetail => roleDetail.Id == addRoleToUserViewModel.RoleId)).Role);

            // Map user object into user profile view model
            var userProfileViewModel = _mapper.Map<UserProfileViewModel>(userObject);

            // Create new record in the role detail user profile table to assign role to the user
            await databaseContext.RoleDetailUserProfiles
                .AddAsync(new RoleDetailUserProfile
                {
                    RoleDetailId = addRoleToUserViewModel.RoleId,
                    UserProfileId = addRoleToUserViewModel.UserId
                });

            // Assign role to a user
            var result = await _userManager.AddToRoleAsync(userObject.User, roleObject.RoleName);

            if (result.Succeeded)
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("user", userProfileViewModel);
                responseData.Add("role", roleObject);
            }
            else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "There's an error while assigning role");
            }

            return new JsonResult(responseData);
        }

        // The function to remove role from a user with specified user id
        [HttpPost("removeRoleFromAUser")]
        public async Task<JsonResult> RemoveRoleFromAUser ([FromBody] RoleUserViewModel removeRoleFromAUserViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get user object of the user to assign role to
            var userObject = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.Id == removeRoleFromAUserViewModel.UserId);

            //// Reference the database to get role object of the role that will be removed from the user
            //// Also map it to the role view model
            //var roleViewModel = _mapper.Map<RoleViewModel>((await databaseContext.RoleDetails
            //    .Include(roleDetail => roleDetail.Role)
            //    .FirstOrDefaultAsync(roleDetail => roleDetail.Id == removeRoleFromAUserViewModel.RoleId)).Role);

            //// Reference the database to get role detail user profile object which is going to be removed
            //var roleDetailUserProfileObjectToBeRemoved = await databaseContext.RoleDetailUserProfiles
            //    .Where(roleDetailUserProfile => roleDetailUserProfile.RoleDetailId == removeRoleFromAUserViewModel.RoleId)
            //    .Where(roleDetailUserprofile => roleDetailUserprofile.UserProfileId == removeRoleFromAUserViewModel.UserId)
            //    .FirstOrDefaultAsync();

            // Reference the database to get role detail user profile object which is going to be removed
            var roleDetailUserProfileObject = await databaseContext.RoleDetailUserProfiles
                .Where(r => r.Id == removeRoleFromAUserViewModel.Id)
                .Include(r => r.UserProfile.User)
                .Include(r => r.RoleDetail.Role)
                .FirstOrDefaultAsync();

            // Map user object into user view model
            var userViewModel = _mapper.Map<UserProfileViewModel>(userObject);

            // Delete the role detail user profile object associated with the role which need to be removed from the user
            databaseContext.RoleDetailUserProfiles.Remove(roleDetailUserProfileObject);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Remove role from a user
            IdentityResult removeRoleResult = await _userManager.RemoveFromRolesAsync(roleDetailUserProfileObject.UserProfile.User, new List<string> { roleDetailUserProfileObject.RoleDetail.Role.Name });

            if (removeRoleResult.Succeeded)
            {
                // Add data to the response data
                responseData.Add("status", "Done.");
                responseData.Add("role", "Role is removed from user");
            }
            else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "Role is not removed from user");
            }

            return new JsonResult(responseData);
        }

        // The function to get all roles in the system
        [HttpGet("getAllRoles")]
        public async Task<JsonResult> GetAllRolesAsync ()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Read all roles into a list of strings
            List<string> roles = _roleManager.Roles.Select(role => role.Name).ToList();

            // The database context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get list of role details and include role object as well
            var roleDetails = await databaseContext.RoleDetails
                .Include(roleDetail => roleDetail.Role)
                .ToListAsync();

            // Map list of role details into list of role view model
            var roleData = _mapper.Map<List<RoleViewModel>>(roleDetails);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", roleData);

            return new JsonResult(responseData);
        }

        // The function to remove a rolet
        // After calling this function, user assigned to a role which is going to be removed
        // will lose that role as well
        [HttpDelete("deleteRole")]
        public async Task<JsonResult> DeleteRole ([FromBody] DeleteRoleViewModel deleteRoleViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Database context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get role detail user profile entity that contains role which going to be removed
            var roleDetailUserProfileData = (await databaseContext.RoleDetailUserProfiles
                .Where(roleDetailUserProfile => roleDetailUserProfile.RoleDetail.Id == deleteRoleViewModel.RoleDetailId)
                .Include(roleDetailUserProfile => roleDetailUserProfile.RoleDetail).ThenInclude(roleDetail => roleDetail.Role)
                .Include(roleDetailUserProfile => roleDetailUserProfile.UserProfile).ThenInclude(userProfile => userProfile.User)
                .ToListAsync())[0];

            // Reference the database to get user role entity that contains role which is going to be removed
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
            responseData.Add("status", "Done");
            responseData.Add("data", "Role is removed");

            return new JsonResult(responseData);
        }
    }
}
