using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Data;
using Microsoft.EntityFrameworkCore;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        // User manager
        private readonly UserManager<User> UserManager;

        // Constructor
        public UserController(UserManager<User> UserManager)
        {
            this.UserManager = UserManager;
        }

        // The view where user can see account info
        [HttpGet("accountInfo")]
        public async Task<IActionResult> AccountInfo()
        {
            ViewData["Header"] = "Account info";

            // Get user id of the currently logged in user
            string currentUserId = UserManager.GetUserId(HttpContext.User);

            // The database context
            var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var currentUserObject = (await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Where(userProfile => userProfile.User.Id == currentUserId)
                .ToListAsync())[0];

            // Initialize view model
            var UserViewModel = new UserViewModel
            {
                Email = currentUserObject.User.Email,
                FullName = currentUserObject.FullName
            };

            return View(UserViewModel);
        }
    }
}
