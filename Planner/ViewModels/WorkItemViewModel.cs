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

        // Full name of the creator
        public string CreatorFullName { get; set; }

        // Date created of the work item
        public string DateCreated { get; set; }
    }
}
