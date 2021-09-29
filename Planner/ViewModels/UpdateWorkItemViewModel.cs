using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class UpdateWorkItemViewModel
    {
        // Title to be updated
        [Required]
        public string Title { get; set; }

        // Content to be updated
        [Required]
        public string Content { get; set; }
    }
}