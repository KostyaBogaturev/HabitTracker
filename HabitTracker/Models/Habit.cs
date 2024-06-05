using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class Habit
    {
        [Key]
        public int HabitId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public TimeSpan TimeOfDay { get; set; }

        public ICollection<HabitTracking> HabitTrackings { get; set; }
    }
}
