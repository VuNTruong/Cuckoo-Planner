using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Planner.Data;
using Planner.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/WorkItem")]
    [ApiController]
    public class WorkItemAPIController : Controller
    {
        // User manager and sign in manager
        private readonly UserManager<User> userManager;

        public WorkItemAPIController(UserManager<User> userManager)
        {
            // Initialize user manager with DI
            this.userManager = userManager;
        }

        // The function to get all work items in the database
        [HttpGet]
        public async Task<JsonResult> getWorkItems()
        {
            using (var db = new DatabaseContext())
            {
                // Start querying the database
                var workItems = await db.WorkItems
                    .ToListAsync();

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", workItems);

                // Return response to the client
                return new JsonResult(responseData);
            }
        }

        // The function to get all work items of the currently logged in user
        [HttpGet("getWorkItemsOfCurrentUser")]
        public async Task<JsonResult> getWorkItemsOfCurrentUser()
        {
            using (var db = new DatabaseContext())
            {
                // Get user id of the currently logged in user
                string currentUserId = userManager.GetUserId(HttpContext.User);

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Reference the database to get work items created by the currently logged in user
                var listOfWorkItems = await db.WorkItems.Where((workItem) =>
                    workItem.creator == currentUserId
                ).ToListAsync();

                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", listOfWorkItems);

                // Return response to the client
                return new JsonResult(responseData);
            }
        }

        // The function to create new work item in the database
        [HttpPost]
        public async Task<JsonResult> createWorkItem()
        {
            using (var db = new DatabaseContext())
            {
                // Use this to read request body
                var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

                // Convert JSON object into accessible object
                var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

                // Get user id of the currently logged in user
                string currentUserId = userManager.GetUserId(HttpContext.User);

                // Reference the database to get user object of the currently logged in user
                var userObject = (await db.Users.Where((userObject) =>
                    userObject.Id == currentUserId
                ).ToListAsync())[0];

                // Create the new object which is going to be inserted into the database
                WorkItem newWorkItem = new WorkItem(requestBody["title"], requestBody["content"], requestBody["dateCreated"], userObject.Id);

                // Start querying the database
                await db.WorkItems
                    .AddAsync(newWorkItem);

                // Save changes
                await db.SaveChangesAsync();

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Add data to the response data
                Response.StatusCode = 201;
                responseData.Add("status", "Done");
                responseData.Add("data", newWorkItem);

                // Return response to the client
                return new JsonResult(responseData);
            }
        }

        //The function to delete a work item in the database
        [HttpDelete]
        public async Task<JsonResult> deleteWorkItem()
        {
            // Get id of work item to be deleted
            int workItemIdToBeDeleted = int.Parse(Request.Query["workItemId"]);

            using (var db = new DatabaseContext())
            {
                // Reference the database to get work item object of the item to be
                // deleted
                var workItemToBeDeleted = db.WorkItems
                    .Single(workitem => workitem.Id == workItemIdToBeDeleted);

                // Delete the found work item
                db.Remove(workItemToBeDeleted);

                // Save changes in the database
                await db.SaveChangesAsync();

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", "Task has been deleted");

                // Return response to the client
                return new JsonResult(responseData);
            }
        }

        // The function to update a work item in the database
        [HttpPatch]
        public async Task<JsonResult> updateWorkItem()
        {
            // Get id of work item to be updated
            int workItemIdToBeUpdated = int.Parse(Request.Query["workItemId"]);

            object type = workItemIdToBeUpdated.GetType();

            using (var db = new DatabaseContext())
            {
                // Use this to read request body
                var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

                // Convert JSON object into accessible object
                var requestBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputData);

                // Reference the database to get object of the work item to be updated
                var workItemToBeUpdated = db.WorkItems
                    .Single((workItem) => workItem.Id == workItemIdToBeUpdated);

                // Make changes on the work item based on request body
                foreach (KeyValuePair<string, object> entry in requestBody)
                {
                    if (entry.Key == "title")
                    {
                        workItemToBeUpdated.title = (string)entry.Value;
                    }
                    else if (entry.Key == "content")
                    {
                        workItemToBeUpdated.content = (string)entry.Value;
                    }
                    else if (entry.Key == "dateCreated")
                    {
                        workItemToBeUpdated.dateCreated = (string)entry.Value;
                    }
                    else if (entry.Key == "doneStatus")
                    {
                        workItemToBeUpdated.doneStatus = (bool)entry.Value;
                    }
                }

                // Update changes in the database
                await db.SaveChangesAsync();

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", workItemToBeUpdated);

                // Return response to the client
                return new JsonResult(responseData);
            }
        }
    }
}
