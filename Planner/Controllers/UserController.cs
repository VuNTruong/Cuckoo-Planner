using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Services;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        // Auto mapper
        private IMapper _mapper;

        // Database context service
        private readonly IDatabaseContext _databaseContextEntities;
       
        // Constructor
        public UserController (IMapper mapper, IDatabaseContext databaseContextEntities)
        {
            // Initialize auto mapper
            _mapper = mapper;

            // Initialize database context service
            _databaseContextEntities = databaseContextEntities;
        }

        // The view where user can see account info
        [HttpGet("accountInfo")]
        public async Task<IActionResult> AccountInfo()
        {
            ViewData["Header"] = "Account info";

            // Get user id of the currently logged in user
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // The database context
            var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var userObject = await _databaseContextEntities.GetUserProfileEntity()
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.User.Id == currentUserId);

            // Map user object into user view model
            var userViewModel = _mapper.Map<UserProfileViewModel>(userObject);

            return View(userViewModel);
        }

        // The view where user can see list of users (for admin)
        [HttpGet("userManager")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UserManager()
        {
            ViewData["Header"] = "User manager";

            // The database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get list of all users
            var listOfUsers = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Include(userProfile => userProfile.RoleDetailUserProfiles).ThenInclude(r => r.RoleDetail).ThenInclude(roleDetail => roleDetail.Role)
                .ToListAsync();

            // Map list of user profile objects into list of user profile view models
            var listOfUserViewModels = _mapper.Map<List<UserProfileViewModel>>(listOfUsers);

            // Return the view
            ViewData["Users"] = listOfUserViewModels;
            return View();
        }
    }
}
