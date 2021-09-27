using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class WorkItemController : Controller
    {
        // Current user utils (this wil be used to get user id of the currently logged in user)
        private readonly CurrentUserUtils currentUserUtils;

        // List of work item view models
        private List<WorkItemViewModel> listOfWorkItemViewModels;

        // Constructor
        public WorkItemController(IHttpContextAccessor httpContextAccessor)
        {
            // Initialize current user utils
            currentUserUtils = new CurrentUserUtils(httpContextAccessor);

            // Initialize work item view model
            listOfWorkItemViewModels = new List<WorkItemViewModel>();
        }

        [HttpGet("")]
        public async Task<IActionResult> WorkItemOverview()
        {
            ViewData["Header"] = "Hello there!";

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Reference the database to get work items created by the currently logged in user
            List<WorkItem> listOfWorkItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // List of work item view models
            //List<WorkItemViewModel> workItemViewModels = new List<WorkItemViewModel>();

            // Loop through list of obtained work items and create work item
            // view model out of them
            foreach (var workItem in listOfWorkItems)
            {
                WorkItemViewModel newWorkItemViewModel = new WorkItemViewModel
                {
                    Title = workItem.Title,
                    Content = workItem.Content,
                    Id = workItem.Id
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
            return View(workItemListViewModel);
        }

        [HttpPost("addMoreWorkItem")]
        public async Task<IActionResult> WorkItemAddMore()
        {
            // Database context
            var databaseContext = new DatabaseContext();

            // Use this to read request body
            var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

            // Convert JSON object into accessible object
            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Create the new object which is going to be inserted into the database
            WorkItem newWorkItem = new(requestBody["title"], requestBody["content"], requestBody["dateCreated"], currentUserId);

            // Start querying the database
            await databaseContext.WorkItems
                .AddAsync(newWorkItem);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Create new view model out of the new work item
            WorkItemViewModel newWorkItemViewModel = new WorkItemViewModel
            {
                Title = newWorkItem.Title,
                Content = newWorkItem.Content,
                Id = newWorkItem.Id
            };

            // Add the newly created work item view model into the list
            listOfWorkItemViewModels.Add(newWorkItemViewModel);

            // Create the new work item list view model
            var workItemListViewModel = new WorkItemListViewModel
            {
                WorkItems = listOfWorkItemViewModels
            };

            // Return the view with updated view model
            return View(workItemListViewModel);
        }
    }
}
