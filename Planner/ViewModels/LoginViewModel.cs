using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Planner.ViewModels
{
    public class LoginViewModel
    {
        // Login email
        [Required]
        public string Email { get; set; }

        // Login password
        [Required]
        public string Password { get; set; }

        // List of login validation error
        public List<string> LoginValidationErrors { get; set; }
    }
}
