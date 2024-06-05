using Microsoft.AspNetCore.Identity;
namespace HabitTracker.Models
{
    public class User : IdentityUser
    {
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }

        public ICollection<Habit> Habits { get; set; }
    }
}
