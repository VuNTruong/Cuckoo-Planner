using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> WorkItemManager([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            ViewData["Header"] = "All work items";

            // The databasse context
            DatabaseContext databaseContext = new DatabaseContext();

            // Default page number is 1
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            // Default page size is 1
            if (pageSize == 0)
            {
                pageSize = 1;
            }

            var workItems = await databaseContext.WorkItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Map list of work item models into list of work item view models
            List<WorkItemViewModel> workItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            ViewData["WorkItems"] = workItemViewModels;
            ViewData["PageSize"] = pageSize;
            ViewData["CurrentPage"] = pageNumber;
            return View();
        }
    }
}
