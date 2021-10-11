using System;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;

namespace Planner.Services
{
    public class DatabaseContextService : IDatabaseContext
    {
        // The database context
        DatabaseContext databaseContext;

        // Initialize database context
        public DatabaseContextService ()
        {
            databaseContext = new DatabaseContext();
        }

        public DbSet<UserProfile> GetUserProfileEntity()
        {
            // Return User profile entity
            return databaseContext.UserProfiles;
        }
    }
}
