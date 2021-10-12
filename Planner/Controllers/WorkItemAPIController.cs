using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.ViewModels;
using AutoMapper;
using Planner.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/WorkItem")]
    [ApiController]
    public class WorkItemAPIController : Controller
    {
        // Current user service (this will be used to get user id of the currently logged in user)
        private readonly ICurrentUser _currentUserService;

        // WorkItem object which will be used when performing CRUD operations with work items
        public WorkItem WorkItem { get; set; }

        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public WorkItemAPIController(IMapper mapper, ICurrentUser currentUserService)
        {
            // Initialize current user service
            _currentUserService = currentUserService;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get all work items in the database
        [HttpGet]
        public async Task<JsonResult> GetWorkItems(int cursor, int amountOfRecords, string loadMode)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get last work item in the table
            var lastWorkItem = await databaseContext.WorkItems
                .OrderByDescending(workItem => workItem.Id)
                .FirstAsync();

            // Reference the database to get first work item in the table
            var firstWorkItem = await databaseContext.WorkItems
                .FirstAsync();

            // Queryable object
            IQueryable<WorkItem> query;

            // Load mode is forward by default
            if (loadMode == null)
            {
                loadMode = "forward";
            }

            // Forward
            if (loadMode == "forward")
            {
                // If cursor position is 0, load from beginning
                if (cursor != 0)
                {
                    query = databaseContext.WorkItems
                        .Include(workItem => workItem.Creator)
                        .Where(workItem => workItem.Id > cursor)
                        .Take(amountOfRecords);
                }
                else
                {
                    query = databaseContext.WorkItems
                        .Include(workItem => workItem.Creator)
                        .Take(amountOfRecords);
                }
            }
            // Backward
            else
            {
                query = databaseContext.WorkItems
                    .OrderByDescending(workItem => workItem.Id)
                    .Include(workItem => workItem.Creator)
                    .Where(workItem => workItem.Id < cursor)
                    .Take(amountOfRecords);
            }

            // Start querying the database
            List<WorkItem> workItems = await query.ToListAsync();

            // List of work item view models
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            // New forward cursor position
            int newForwardCursorPosition = 0;

            // New backward cursor posititon
            int newBackwardCursorPosition = 0;

            // Forward
            if (loadMode == "forward")
            {
                // New backward cursor position will be first element of the list
                newBackwardCursorPosition = workItems[0].Id;

                // If number of found work items is less than amount of desired records, user is already at
                // the end of the table
                if (workItems.Count < amountOfRecords)
                {
                    // New forward cursor will be -1 which indicates the end is reached   
                    newForwardCursorPosition = -1;
                }
                else
                {
                    if (workItems[amountOfRecords - 1].Id == lastWorkItem.Id)
                    {
                        // If user is already at the end of table (last work item id in the list will
                        // equal to work item id of the last record in the table). New forward cursor
                        // will be -1 which indicates the end is reached
                        newForwardCursorPosition = -1;
                    }
                    else
                    {
                        // Id of the last element in workItems list will be the new forward cursor position
                        newForwardCursorPosition = workItems[amountOfRecords - 1].Id;
                    }
                }

                // List of work item view models
                listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);
            }
            // Backward
            else
            {
                // Reverse list of work items
                workItems.Reverse();

                // New forward cursor position will be last element of the list
                newForwardCursorPosition = workItems[workItems.Count - 1].Id;

                if (workItems.Count < amountOfRecords)
                {
                    // If number of found work items is less than amount of desired records, user is already at
                    // the end of the table
                    newBackwardCursorPosition = -1;
                }
                else
                {
                    if (workItems[0].Id == firstWorkItem.Id)
                    {
                        // If user is already at the beginning of table (first work item id in the list will
                        // equal to work item id of the first record in the table). New backward cursor
                        // will be -1 which indicates the beginning is reached
                        newBackwardCursorPosition = -1;
                    }
                    else
                    {
                        // Id of the first element in workItems list will be the new cursor position
                        newBackwardCursorPosition = workItems[0].Id;
                    }
                }

                // List of work item view models
                listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);
            }

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", listOfWorkItemViewModels);
            responseData.Add("newForwardCursorPosition", newForwardCursorPosition);
            responseData.Add("newBackwardCursorPosition", newBackwardCursorPosition);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to get all work items of the currently logged in user
        [HttpGet("getWorkItemsOfCurrentUser")]
        public async Task<JsonResult> GetWorkItemsOfCurrentUser()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await _currentUserService.GetCurrentUserId();

            // Reference the database to get work items created by the currently logged in user
            var workItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Map list of work item models into list of work item view models
            List<WorkItemViewModel> listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", listOfWorkItemViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to create new work item in the database
        [HttpPost]
        public async Task<JsonResult> CreateWorkItem([FromBody] WorkItemViewModel newWorkItemViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // The database context
            var databaseContext = new DatabaseContext();

            // Call the function to get user if of the currently logged in user
            int currentUserId = await _currentUserService.GetCurrentUserId();

            // Map the new work item view model into new work item object
            WorkItem newWorkItemObject = _mapper.Map<WorkItem>(newWorkItemViewModel);
            newWorkItemObject.CreatorId = currentUserId;

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
            var workItemToBeDeleted = await databaseContext.WorkItems
                .FirstOrDefaultAsync(workitem => workitem.Id == workItemId);

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
        public async Task<JsonResult> UpdateWorkItem([FromBody] WorkItemViewModel updateWorkItemViewModel)
        {
            // Get id of work item to be updated
            int workItemIdToBeUpdated = int.Parse(Request.Query["workItemId"]);

            // Database context
            var databaseContext = new DatabaseContext();

            // Reference the database to get object of the work item to be updated
            var workItemToBeUpdated = await databaseContext.WorkItems
                .FirstOrDefaultAsync((workItem) => workItem.Id == workItemIdToBeUpdated);

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
