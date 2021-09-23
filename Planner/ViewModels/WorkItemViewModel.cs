using System;
using System.Collections.Generic;
using Planner.Models;

namespace Planner.ViewModels
{
    public class WorkItemViewModel
    {
        // List of work items of the currently logged in user
        public List<WorkItem> WorkItems { get; set; }
    }
}
