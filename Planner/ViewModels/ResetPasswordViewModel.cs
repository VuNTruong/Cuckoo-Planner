using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Planner.ViewModels
{
    public class ResetPasswordViewModel
    {
        // Email to get password reset token
        [Required]
        public string Email { get; set; }

        // List of validation errors
        public List<string> ValidationErrors { get; set; }
    }
}
