using System;
namespace Planner.ViewModels
{
    public class WorkItemViewModel
    {
        // Id of the work item
        public int Id { get; set; }

        // Title of the work item
        public string Title { get; set; }

        // Content of the work item
        public string Content { get; set; }
    }
}
