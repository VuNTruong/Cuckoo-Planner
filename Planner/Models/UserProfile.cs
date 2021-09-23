using System;
using System.Collections.Generic;

namespace Planner.Models
{
    public class UserProfile
    {
        // User profile id (this will be used to connect with User table)
        public int Id { get; set; }

        // User full name
        public string FullName { get; set; }

        // One UserProfile will have only one Identity
        public User User { get; set; }

        // One UserProfile will have many work items
        public virtual List<WorkItem> WorkItems { get; set; }
    }
}
