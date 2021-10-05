namespace Planner.ViewModels
{
    public class RoleAssignmentViewModel
    {
        // Id of the the role detail user profile item
        public int Id { get; set; }

        // Name of the user that get assigned
        public string UserFullNameGetAssigned { get; set; }

        // Name of role assigned to user
        public string RoleNameAssignedToUser { get; set; }

        // User id of the user that get assigned
        public string UserIdGetAssigned { get; set; }

        // Role name that the user get assigned to
        public string AssignedRole { get; set; }
    }
}
