using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class TimingLocationWorkEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DayOfWeekEntity DayOfWeek { get; set; }

        [Required]
        public WorkTimeEntity WorkTime { get; set; }
    }
}