using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Planner.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }

        public Role()
        {
        }

        public Role(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
    }
}
