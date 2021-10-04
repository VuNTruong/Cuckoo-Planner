using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Planner.Data;
using Planner.Models;
using Planner.Utils;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("main")]
    [Authorize]
    public class WorkItemController : Controller
    {
        // WorkItem object which will be used when performing CRUD operations with work items
        //[BindProperty]
        //[FromBody]
        //public WorkItem WorkItem { get; set; }

        // Current user utils (this wil be used to get user id of the currently logged in user)
        private readonly CurrentUserUtils currentUserUtils;

        // Constructor
        public WorkItemController(IHttpContextAccessor httpContextAccessor)
        {
            // Initialize current user utils
            currentUserUtils = new CurrentUserUtils(httpContextAccessor);
        }

        [HttpGet("")]
        public async Task<IActionResult> WorkItemOverview()
        {
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            ViewData["Header"] = "Hello there!";

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Reference the database to get work items created by the currently logged in user
             List<WorkItem> listOfWorkItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Loop through list of obtained work items and create work item
            // view model out of them
            foreach (var workItem in listOfWorkItems)
            {
                WorkItemViewModel newWorkItemViewModel = new WorkItemViewModel
                {
                    Title = workItem.Title,
                    Content = workItem.Content,
                    Id = workItem.Id,
                    DateCreated = workItem.DateCreated
                };

                // Add the newly created work item view model into the list
                listOfWorkItemViewModels.Add(newWorkItemViewModel);
            }

            // Initialize view model
            var workItemListViewModel = new WorkItemListViewModel
            {
                WorkItems = listOfWorkItemViewModels
            };

            // Return the view with updated view model
            return View("WorkItemOverview", workItemListViewModel);
        }

        [HttpPost("addMoreWorkItem")]
        public async Task<IActionResult> WorkItemAddMore(WorkItemViewModel workItemViewModel)
        {
            // List of work item view models
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            // This object is used to get current day
            string currentDate = DateTime.UtcNow.ToString("MM - dd - yyyy");

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Work item object to be added to the database
            WorkItem newWorkItem = new WorkItem
            {
                Title = workItemViewModel.Title,
                Content = workItemViewModel.Content,
                DateCreated = currentDate,
                CreatorId = currentUserId
            };

            // Start querying the database
            await databaseContext.WorkItems
                .AddAsync(newWorkItem);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Reference the database to get work items created by the currently logged in user
            List<WorkItem> listOfWorkItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Loop through list of obtained work items and create work item
            // view model out of them
            foreach (var workItem in listOfWorkItems)
            {
                WorkItemViewModel newWorkItemViewModel = new WorkItemViewModel
                {
                    Title = workItem.Title,
                    Content = workItem.Content,
                    Id = workItem.Id,
                    DateCreated = workItem.DateCreated
                };

                // Add the newly created work item view model into the list
                listOfWorkItemViewModels.Add(newWorkItemViewModel);
            }

            // Initialize view model
            var workItemListViewModel = new WorkItemListViewModel
            {
                WorkItems = listOfWorkItemViewModels
            };

            // Return the view with updated view model
            ViewData["Header"] = "Hello there!";
            return View("WorkItemOverview", workItemListViewModel);
        }
    }
}
