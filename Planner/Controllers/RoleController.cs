using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        // Role manager
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // The function to get all roles in the system
        public IActionResult RoleOverview()
        {
            // List of role view models
            List<RoleViewModel> listOfRoleViewModels = new List<RoleViewModel>();

            // Read all roles into list of role view model
            foreach(var roleObject in _roleManager.Roles)
            {
                // Create the role view model object out of the role
                RoleViewModel roleViewModel = new RoleViewModel {
                    RoleId = roleObject.Id,
                    RoleName = roleObject.Name
                };

                // Add the newly created role view model object into the list
                listOfRoleViewModels.Add(roleViewModel);
            }

            // Create the role list view model
            RoleListViewModel roleListViewModel = new RoleListViewModel
            {
                ListOfRolesViewModels = listOfRoleViewModels
            };

            return View(roleListViewModel);
        }

        // The function to get role assignments
        [HttpGet("roleAssignment")]
        public async Task<IActionResult> RoleAssignmentAsync()
        {
            // List of role assignment view models
            List<RoleAssignmentViewModel> listOfRoleAssignmentViewModels = new List<RoleAssignmentViewModel>();

            // The database context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get list of role assignments in the database
            var roleAssignments = await databaseContext.RoleDetailUserProfiles
                .Include(roleDetailUserProfile => roleDetailUserProfile.UserProfile)
                .Include(roleDetailUserProfile => roleDetailUserProfile.RoleDetail)
                .ThenInclude(roleDetail => roleDetail.Role).ToListAsync();

            foreach (var roleAssignment in roleAssignments)
            {
                // Create the role assignment view model out of the role assignment
                RoleAssignmentViewModel roleAssignmentViewModel = new RoleAssignmentViewModel
                {
                    UserFullNameGetAssigned = roleAssignment.UserProfile.FullName,
                    RoleNameAssignedToUser = roleAssignment.RoleDetail.Role.Name
                };

                // Add the created role assignment view model into the list
                listOfRoleAssignmentViewModels.Add(roleAssignmentViewModel);
            }

            // Create the role assignment list view model
            RoleAssignmentListViewModel roleAssignmentListViewModel = new RoleAssignmentListViewModel
            {
                RoleAssignments = listOfRoleAssignmentViewModels
            };

            return View(roleAssignmentListViewModel);
        }
    }
}
