using System;
using Microsoft.AspNetCore.Identity;

namespace Planner.Models
{
    public class User : IdentityUser
    {
        // Use profile id which will be used to link this with UserProfile table
        // This is also known as principal key
        public int UserProfileId { get; set; }

        // One User object will only have one UserProfile object
        public UserProfile UserProfile { get; set; }
    }
}
