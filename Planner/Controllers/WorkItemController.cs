using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.Services;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("main")]
    [Authorize]
    public class WorkItemController : Controller
    {
        // Current user service (this will be used to get user id of the currently logged in user)
        private readonly ICurrentUser _currentUserService;

        // Auto mapper
        private readonly IMapper _mapper;

        // Constructor
        public WorkItemController(IMapper mapper, ICurrentUser currentUserService)
        {
            // Initialize current user service
            _currentUserService = currentUserService;

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get work items of the currently logged in user
        [HttpGet("")]
        public async Task<IActionResult> WorkItemOverview()
        {
            ViewData["Header"] = "Hello there!";

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await _currentUserService.GetCurrentUserId();

            // Reference the database to get work items created by the currently logged in user
            List<WorkItem> listOfWorkItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Map list of work item models into list of work item view models
            List<WorkItemViewModel>  listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(listOfWorkItems);

            ViewData["WorkItems"] = listOfWorkItemViewModels;
            // Return the view with updated view model
            return View();
        }

        // The function to get all work items in the database
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetAllWorkItems")]
        public async Task<IActionResult> WorkItemManager([FromQuery] int cursor, [FromQuery] int amountOfRecords, [FromQuery] string loadMode)
        {
            ViewData["Header"] = "All work items";

            // The databasse context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get last work item in the table
            var lastWorkItem = await databaseContext.WorkItems
                .OrderByDescending(workItem => workItem.Id)
                .FirstAsync();

            // Reference the database to get first work item in the table
            var firstWorkItem = await databaseContext.WorkItems
                .FirstAsync();

            // Queryable object
            IQueryable <WorkItem> query;

            // Load mode is null by default
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
                    } else
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
                    } else
                    {
                        // Id of the first element in workItems list will be the new cursor position
                        newBackwardCursorPosition = workItems[0].Id;
                    }
                }

                // List of work item view models
                listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);
            }

            // Return the view with updated view model
            ViewData["WorkItems"] = listOfWorkItemViewModels;
            ViewData["PageSize"] = amountOfRecords;
            ViewData["NewForwardCursorPosition"] = newForwardCursorPosition;
            ViewData["LoadMode"] = loadMode;
            ViewData["CurrentCursor"] = cursor;
            ViewData["NewBackwardCursorPosition"] = newBackwardCursorPosition;
            return View();
        }
    }
}
