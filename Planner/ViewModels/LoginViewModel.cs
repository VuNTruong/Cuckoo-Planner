using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Planner.ViewModels
{
    public class LoginViewModel
    {
        // Login email
        [Required]
        [FromBody]
        public string Email { get; set; }

        // Login password
        [FromBody]
        public string Password { get; set; }
    }
}
