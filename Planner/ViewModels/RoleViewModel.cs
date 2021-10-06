using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class RoleViewModel
    {
        // Id of the role
        public string RoleId { get; set; }

        // Description of the role
        public string RoleDescription { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
