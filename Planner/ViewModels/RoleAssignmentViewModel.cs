using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Planner.Data;

namespace Planner.ViewModels
{
    public class RoleAssignmentViewModel
    {
        // Name of the user that get assigned
        public string UserFullNameGetAssigned { get; set; }

        // Name of role assigned to user
        public string RoleNameAssignedToUser { get; set; }

        // The function to get user object of the user based on user id
        public async Task GetUserFullName (string userId)
        {
        
        }

        // User id of the user that get assigned
        public string UserIdGetAssigned { get; set; }

        // Role name that the user get assigned to
        public string AssignedRole { get; set; }
    }
}
