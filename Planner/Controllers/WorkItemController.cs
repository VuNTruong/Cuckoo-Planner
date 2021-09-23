using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("dashboard")]
    public class WorkItemController : Controller
    {
        // User manager
        private readonly UserManager<User> UserManager;

        // Constructor
        public WorkItemController(UserManager<User> UserManager)
        {
            this.UserManager = UserManager;
        }

        // The function to get user id of the currently logged in user (numeric)
        public async Task<int> GetCurrentUserId()
        {
            // Get user id of the currently logged in user
            string currentUserId = UserManager.GetUserId(HttpContext.User);

            // The database context
            var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var currentUserObject = (await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Where(userProfile => userProfile.User.Id == currentUserId)
                .ToListAsync())[0];

            // Return thre obtained user id
            return currentUserObject.Id;
        }

        [HttpGet("main")]
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = "Hello there!";

            // Database context
            var DatabaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int CurrentUserId = await GetCurrentUserId();

            // Reference the database to get work items created by the currently logged in user
            List<WorkItem> ListOfWorkItems = await DatabaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == CurrentUserId
            ).ToListAsync();

            // Initialize view model
            var WorkItemViewModel = new WorkItemViewModel
            {
                WorkItems = ListOfWorkItems
            };

            // Return the view with updated view model
            return View(WorkItemViewModel);
        }
    }
}
