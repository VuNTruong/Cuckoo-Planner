using System;
using Microsoft.EntityFrameworkCore;
using Planner.Models;

namespace Planner.Services
{
    public interface IDatabaseContext
    {
        public DbSet<UserProfile> GetUserProfileEntity();
    }
}
