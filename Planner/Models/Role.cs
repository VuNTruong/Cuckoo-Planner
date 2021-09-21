using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Planner.Models
{
    public class Role : IdentityRole
    {
        public string description { get; set; }
        public Role()
        {
        }

        public Role(string roleName, string description) : base(roleName)
        {
            this.description = description;
        }
    }
}
