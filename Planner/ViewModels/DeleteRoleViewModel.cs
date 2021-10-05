using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class DeleteRoleViewModel
    {
        [Required]
        public int RoleDetailId { get; set; }
    }
}
