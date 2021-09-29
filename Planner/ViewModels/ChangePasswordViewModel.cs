using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class ChangePasswordViewModel
    {
        // Current password
        [Required]
        public string CurrentPassword { get; set; }

        // New password
        [Required]
        public string NewPassword { get; set; }

        // New password confirm
        [Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }
    }
}
