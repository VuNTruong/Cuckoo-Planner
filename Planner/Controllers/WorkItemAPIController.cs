using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.Utils;
using Microsoft.AspNetCore.Http;
using Planner.ViewModels;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/WorkItem")]
    [ApiController]
    public class WorkItemAPIController : Controller
    {
        // WorkItem object which will be used when performing CRUD operations with work items
        public WorkItem WorkItem { get; set; }

        // Current user utils (this will be used to get user id of the currently logged in user)
        private readonly CurrentUserUtils currentUserUtils;

        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public WorkItemAPIController(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            // Initialize current user utils
            currentUserUtils = new CurrentUserUtils(httpContextAccessor);

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get all work items in the database
        [HttpGet]
        public async Task<JsonResult> GetWorkItems()
        {
            // List of work item manager view models
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Start querying the database
            var workItems = await databaseContext.WorkItems
                .Include(workItem => workItem.Creator)
                .ToListAsync();

            // Map list of work items into list of work item view models
            listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", listOfWorkItemViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to get all work items of the currently logged in user
        [HttpGet("getWorkItemsOfCurrentUser")]
        public async Task<JsonResult> GetWorkItemsOfCurrentUser()
        {
            // List of work item view models
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Reference the database to get work items created by the currently logged in user
            var workItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Map list of work item models into list of work item view models
            listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", listOfWorkItemViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to create new work item in the database
        [HttpPost]
        public async Task<JsonResult> CreateWorkItem([FromBody] AddWorkItemViewModel addWorkItemViewModel)
        {
            // The database context
            var databaseContext = new DatabaseContext();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get user if of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Create the new work item object
            WorkItem newWorkItemObject = new WorkItem
            {
                Content = addWorkItemViewModel.Content,
                Title = addWorkItemViewModel.Title,
                CreatorId = currentUserId,
                DateCreated = addWorkItemViewModel.DateCreated
            };

            // Start querying the database
            await databaseContext.WorkItems
                .AddAsync(newWorkItemObject);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Map new work item object into a work item view model
            WorkItemViewModel workItemViewModel = _mapper.Map<WorkItemViewModel>(newWorkItemObject);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItemViewModel);

            // Return response to the client
            return new JsonResult(responseData);
        }

        //The function to delete a work item in the database
        [HttpDelete]
        public async Task<JsonResult> DeleteWorkItem(int workItemId)
        {
            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get work item object of the item to be
            // deleted
            var workItemToBeDeleted = databaseContext.WorkItems
                .Single(workitem => workitem.Id == workItemId);

            // Delete the found work item
            databaseContext.Remove(workItemToBeDeleted);

            // Save changes in the database
            await databaseContext.SaveChangesAsync();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", "Task has been deleted");

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to update a work item in the database
        [HttpPatch]
        public async Task<JsonResult> UpdateWorkItem([FromBody] UpdateWorkItemViewModel updateWorkItemViewModel)
        {
            // Get id of work item to be updated
            int workItemIdToBeUpdated = int.Parse(Request.Query["workItemId"]);

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get object of the work item to be updated
            var workItemToBeUpdated = databaseContext.WorkItems
                .Single((workItem) => workItem.Id == workItemIdToBeUpdated);

            // Update the work item
            workItemToBeUpdated.Content = updateWorkItemViewModel.Content;
            workItemToBeUpdated.Title = updateWorkItemViewModel.Title;

            // Update changes in the database
            await databaseContext.SaveChangesAsync();

            // Map the updated work item object into a work item view model
            WorkItemViewModel workItemViewModel = _mapper.Map<WorkItemViewModel>(workItemToBeUpdated);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItemViewModel);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to delete all tasks in the database
        [HttpDelete("deleteEverything")]
        public async void DeleteEverything()
        {
            // The database context
            var db = new DatabaseContext();

            // Execute command and delete all tasks
            await db.Database.ExecuteSqlRawAsync("DELETE FROM workitems");
        }
    }
}
