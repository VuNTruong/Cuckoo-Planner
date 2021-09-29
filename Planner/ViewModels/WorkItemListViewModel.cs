using System;
using System.Collections.Generic;

namespace Planner.ViewModels
{
    public class WorkItemListViewModel
    {
        // List of work items of the currently logged in user
        public List<WorkItemViewModel> WorkItems { get; set; }

        // This is to check if work item update menu should be shown or not
        public bool IsShowingWorkItemUpdateMenu { get; set; }
    }
}