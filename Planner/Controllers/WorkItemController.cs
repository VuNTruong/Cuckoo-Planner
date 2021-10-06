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
using Planner.Utils;
using Planner.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("main")]
    [Authorize]
    public class WorkItemController : Controller
    {
        // Current user utils (this wil be used to get user id of the currently logged in user)
        private readonly CurrentUserUtils currentUserUtils;

        // Auto mapper
        private readonly IMapper _mapper;

        // Constructor
        public WorkItemController(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            // Initialize current user utils
            currentUserUtils = new CurrentUserUtils(httpContextAccessor);

            // Initialize auto mapper
            _mapper = mapper;
        }

        // The function to get work items of the currently logged in user
        [HttpGet("")]
        public async Task<IActionResult> WorkItemOverview()
        {
            ViewData["Header"] = "Hello there!";

            // List of work item view models
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            // Database context
            var databaseContext = new DatabaseContext();

            // Call the function to get info of the currently logged in user
            int currentUserId = await currentUserUtils.GetCurrentUserId();

            // Reference the database to get work items created by the currently logged in user
            List<WorkItem> listOfWorkItems = await databaseContext.WorkItems.Where((workItem) =>
                workItem.CreatorId == currentUserId
            ).ToListAsync();

            // Map list of work item models into list of work item view models
            listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(listOfWorkItems);

            // Initialize view model
            var workItemListViewModel = new WorkItemListViewModel
            {
                WorkItems = listOfWorkItemViewModels
            };

            // Return the view with updated view model
            return View(workItemListViewModel);
        }

        // The function to get all work items in the database
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetAllWorkItems")]
        public async Task<IActionResult> WorkItemManager()
        {
            ViewData["Header"] = "All work items";

            // List of work item view models
            List<WorkItemViewModel> listOfWorkItemViewModels = new List<WorkItemViewModel>();

            // The databasse context
            DatabaseContext databaseContext = new DatabaseContext();

            // Reference the database to get list of all work items
            List<WorkItem> listOfWorkItems = await databaseContext.WorkItems
                .Include(workItem => workItem.Creator).ToListAsync();

            // Map list of work item models into list of work item view models
            listOfWorkItemViewModels = _mapper.Map<List<WorkItemViewModel>>(listOfWorkItems);

            var workItemListViewModel = new WorkItemListViewModel
            {
                WorkItems = listOfWorkItemViewModels
            };

            // Return the view with updated view model
            return View(workItemListViewModel);
        }
    }
}
