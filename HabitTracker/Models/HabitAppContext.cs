using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace HabitTracker.Models
{
    public class HabitAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitTracking> HabitTrackings { get; set; }

        public HabitAppContext(DbContextOptions<HabitAppContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserId)
                .IsUnique();

            modelBuilder.Entity<Habit>()
                .HasIndex(h => h.HabitId)
                .IsUnique();

            modelBuilder.Entity<HabitTracking>()
                .HasIndex(h => h.HabitTrackingId)
                .IsUnique();
        }

    }
}
