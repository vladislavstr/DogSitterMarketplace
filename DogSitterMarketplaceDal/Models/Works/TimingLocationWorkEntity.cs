using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class TimingLocationWorkEntity
    {
        //public DayOfWeekEntity DayOfWeek { get; set; }

        //public LocationWorkEntity LocationWork { get; set; }

        //public WorkTimeEntity WorkTime { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan Start { get; set; }

        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan Stop { get; set; }

        [Required]
        [ForeignKey(nameof(DayOfWeekId))]
        public DayOfWeekEntity DayOfWeek { get; set; }

        public int DayOfWeekId { get; set; }

        [Required]
        [ForeignKey(nameof(LocationWorkId))]
        public virtual LocationWorkEntity LocationWork { get; set; }

        public int LocationWorkId { get; set; }
    }
}