using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class CreateNewPasswordViewModel
    {
        // Email of the user who need password reset
        [Required]
        public string Email { get; set; }

        // Password reset token
        [Required]
        public string PasswordResetToken { get; set; }

        // New password
        [Required]
        public string NewPassword { get; set; }

        // New password confirm
        [Required]
        [Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }

        // Validation errors
        public List<string> ValidationErrors { get; set; }
    }
}
