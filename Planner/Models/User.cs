using System;
using Microsoft.AspNetCore.Identity;

namespace Planner.Models
{
    public class User : IdentityUser
    {
        public string fullName { get; set; }
    }
}
