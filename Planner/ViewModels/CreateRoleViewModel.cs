using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        // Name of the new role
        public string NewRoleName { get; set; }

        // Description of the new role
        public string NewRoleDescription { get; set; }
    }
}
