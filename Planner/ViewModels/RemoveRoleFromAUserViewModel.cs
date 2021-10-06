using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class RemoveRoleFromAUserViewModel
    {
        [Required]
        // User id of the user who will be removed from a role
        public int UserId { get; set; }

        [Required]
        // Role id of the role that will be removed from a user
        public int RoleId { get; set; }
    }
}
