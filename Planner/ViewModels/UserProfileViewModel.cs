using System;
using System.Collections.Generic;
using Planner.Models;

namespace Planner.ViewModels
{
    public class UserProfileViewModel
    {
        // Id of the user profile object
        public int Id { get; set; }

        // Email of the user to be show
        public string UserEmail { get; set; }

        // Full name of the user to be shown
        public string FullName { get; set; }

        // List of roles that user is in
        public List<RoleDetailUserProfile> RoleDetailUserProfiles { get; set; }
    }
}
