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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=192.168.1.100;Initial Catalog=cuckooplanner;User ID=sa;Password=Test123;TrustServerCertificate=False;MultipleActiveResultSets=True;App=EntityFramework");

        // Configure database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table for Work itemm entity collection should be "workitems"
            modelBuilder.Entity<WorkItem>().ToTable("workitems");

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
