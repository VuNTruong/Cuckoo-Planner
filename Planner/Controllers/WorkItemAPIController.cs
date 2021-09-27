using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Planner.Data;
using Planner.Models;
using Planner.Utils;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/WorkItem")]
    [ApiController]
    public class WorkItemAPIController : Controller
    {
        // WorkItem object which will be used when performing CRUD operations with work items
        [BindProperty]
        [FromBody]
        public WorkItem WorkItem { get; set; }

        // Current user utils (this will be used to get user id of the currently logged in user)
        private readonly CurrentUserUtils currentUserUtils;

        // Constructor
        public WorkItemAPIController(IHttpContextAccessor httpContextAccessor)
        {
            // Initialize current user utils
            currentUserUtils = new CurrentUserUtils(httpContextAccessor);
        }

        // The function to get all work items in the database
        [HttpGet]
        public async Task<JsonResult> GetWorkItems()
        {
            // Database context
            var databaseContext = new DatabaseContext();

            // Start querying the database
            var WorkItems = await databaseContext.WorkItems
                .Include(workItem => workItem.Creator)
                .ToListAsync();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", WorkItems);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to get all work items of the currently logged in user
        [HttpGet("getWorkItemsOfCurrentUser")]
        public async Task<JsonResult> GetWorkItemsOfCurrentUser()
        {
            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Reference the database to get work items created by the currently logged in user
            var listOfWorkItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", listOfWorkItems);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to create new work item in the database
        [HttpPost]
        public async Task<JsonResult> CreateWorkItem()
        {
            // The database context
            var databaseContext = new DatabaseContext();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get user if of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Update user id of the work item creator
            WorkItem.CreatorId = currentUserId;

            // Start querying the database
            await databaseContext.WorkItems
                .AddAsync(WorkItem);

            // Save changes
            await databaseContext.SaveChangesAsync();

            // Add data to the response data
            Response.StatusCode = 201;
            responseData.Add("status", "Done");
            responseData.Add("data", WorkItem);

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
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", "Task has been deleted");

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to update a work item in the database
        [HttpPatch]
        public async Task<JsonResult> UpdateWorkItem()
        {
            // Get id of work item to be updated
            int workItemIdToBeUpdated = int.Parse(Request.Query["workItemId"]);

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get object of the work item to be updated
            var workItemToBeUpdated = databaseContext.WorkItems
                .Single((workItem) => workItem.Id == workItemIdToBeUpdated);

            // Update the work item
            workItemToBeUpdated.Content = WorkItem.Content;
            workItemToBeUpdated.Title = WorkItem.Title;

            // Update changes in the database
            await databaseContext.SaveChangesAsync();

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Add data to the response data
            Response.StatusCode = 200;
            responseData.Add("status", "Done");
            responseData.Add("data", workItemToBeUpdated);

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
