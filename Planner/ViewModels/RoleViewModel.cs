using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
