using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        // Auto mapper
        private readonly IMapper _mapper;

        // Constructor
        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;

            // Initialize mapper
            _mapper = mapper;
        }

        // The function to get all roles in the system
        public IActionResult RoleOverview()
        {
            // Map list of roles into list of role view models
            List<RoleViewModel>  listOfRoleViewModels = _mapper.Map<List<RoleViewModel>>(_roleManager.Roles);

            ViewData["Roles"] = listOfRoleViewModels;
            return View();
        }

        // The function to get role assignments
        [HttpGet("roleAssignment")]
        public async Task<IActionResult> RoleAssignmentAsync()
        {
            // The database context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get list of role assignments in the database
            var roleAssignments = await databaseContext.RoleDetailUserProfiles
                .Include(roleDetailUserProfile => roleDetailUserProfile.UserProfile)
                .Include(roleDetailUserProfile => roleDetailUserProfile.RoleDetail)
                .ThenInclude(roleDetail => roleDetail.Role)
                .ToListAsync();

            // Map list of role assignments into list of role assignment view models
            List<RoleAssignmentViewModel>  listOfRoleAssignmentViewModels = _mapper.Map<List<RoleAssignmentViewModel>>(roleAssignments);

            ViewData["RoleAssignments"] = listOfRoleAssignmentViewModels;
            return View();
        }
    }
}
