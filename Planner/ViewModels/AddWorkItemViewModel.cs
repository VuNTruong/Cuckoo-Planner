using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class AddWorkItemViewModel
    {
        // Title of the work item to be added
        [Required]
        public string Title { get; set; }

        // Content of the work item to be added
        [Required]
        public string Content { get; set; }

        // Date created of the work item to be added
        [Required]
        public string DateCreated { get; set; }
    }
}
