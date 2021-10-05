using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class AddRoleToUserViewModel
    {
        [Required]
        // User id of the user who will get the role
        public int UserId { get; set; }

        [Required]
        // Role id of the role that will be assigned
        public int RoleId { get; set; }
    }
}
