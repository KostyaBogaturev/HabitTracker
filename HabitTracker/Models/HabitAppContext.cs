using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace HabitTracker.Models
{
    public class HabitAppContext : IdentityDbContext<User>
    {
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitTracking> HabitTrackings { get; set; }

        public HabitAppContext(DbContextOptions<HabitAppContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Habit>()
                .HasIndex(h => h.HabitId)
                .IsUnique();

            modelBuilder.Entity<HabitTracking>()
                .HasIndex(h => h.HabitTrackingId)
                .IsUnique();
        }

    }
}
