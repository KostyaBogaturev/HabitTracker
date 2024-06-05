using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FullName { get; set; }

        public string GoogleId { get; set; }
        public string FacebookId { get; set; }

        public ICollection<Habit> Habits { get; set; }
    }
}
