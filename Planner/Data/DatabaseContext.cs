using System;
using Microsoft.EntityFrameworkCore;
using Planner.Models;
using Microsoft.Extensions.Configuration;

namespace Planner.Data
{
    public class DatabaseContext : DbContext
    {
        // Constructor for the Startup.cs file
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DatabaseContext () { }

        // Create Work Item entity
        public DbSet<WorkItem> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=192.168.1.100;Initial Catalog=cuckooplanner;User ID=sa;Password=Test123;TrustServerCertificate=False;MultipleActiveResultSets=True;App=EntityFramework");

        // Configure database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table for Work itemm entity collection should be "workitems"
            modelBuilder.Entity<WorkItem>().ToTable("workitems");
        }
    }
}
