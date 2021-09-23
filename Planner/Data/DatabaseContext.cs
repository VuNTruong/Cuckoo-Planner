using System;
using Microsoft.EntityFrameworkCore;
using Planner.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Planner.Data
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        // Constructor for the Startup.cs file
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DatabaseContext () { }

        // Create Work Item entity
        public DbSet<WorkItem> WorkItems { get; set; }

        // Create user profile entity
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=192.168.1.100;Initial Catalog=cuckooplanner;User ID=sa;Password=Test123;TrustServerCertificate=False;MultipleActiveResultSets=True;App=EntityFramework");

        // Configure database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table for Work itemm entity collection should be "workitems"
            modelBuilder.Entity<WorkItem>().ToTable("workitems");

            // Table for User profile entity collection should be "userprofiles"
            modelBuilder.Entity<UserProfile>().ToTable("userprofiles");

            // Create the relationship between User table and UserProfile table
            // One user profile will have only one identity
            /*
             * One User profile -> one User identity
             UserProfile -------> User
             */
            modelBuilder.Entity<UserProfile>()
                .HasOne(userprofile => userprofile.User)
                .WithOne(user => user.UserProfile)
                .HasForeignKey<User>(user => user.UserProfileId);

            // Create the relationship between User UserProfile table and WorkItem table
            // One user profile will have many work items
            /*
             * One User profile -> many work items
             UserProfile   --------> WorkItem (has one user profile)
             (with many    |-------> WorkItem
             userprofiles) |-------> WorkItem
             */
            modelBuilder.Entity<WorkItem>()
                .HasOne(workItem => workItem.Creator)
                .WithMany(userProfile => userProfile.WorkItems)
                .HasForeignKey(workItem => workItem.CreatorId);

            modelBuilder.Entity<UserProfile>()
                .HasMany(userProfile => userProfile.WorkItems)
                .WithOne(workItem => workItem.Creator)
                .HasPrincipalKey(userProfile => userProfile.Id);

            // Exclude "AspNet" from table names in IdentityDbContext
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
