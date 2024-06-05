using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class HabitTracking
    {
        [Key]
        public int HabitTrackingId { get; set; }

        [Required]
        public int HabitId { get; set; }

        [ForeignKey("HabitId")]
        public Habit Habit { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
    }
}
