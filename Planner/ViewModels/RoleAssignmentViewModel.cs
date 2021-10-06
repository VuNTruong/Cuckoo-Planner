using Planner.Models;

namespace Planner.ViewModels
{
    public class RoleAssignmentViewModel
    {
        // Id of the the role detail user profile item
        public int Id { get; set; }

        // Name of the user that get assigned
        public string UserProfileFullName { get; set; }

        // Role name that the user get assigned to
        public string RoleDetailRoleName { get; set; }
    }
}
