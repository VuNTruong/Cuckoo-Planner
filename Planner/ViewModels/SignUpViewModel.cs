using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Planner.ViewModels
{
    public class SignUpViewModel
    {
        // Full name
        [Required]
        public string FullName { get; set; }

        // Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Password
        [Required]
        public string Password { get; set; }

        // Password confirm
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
