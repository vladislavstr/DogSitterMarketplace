using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class TimingLocationWorkEntity
    {
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

        public override string ToString()
        {
            return $"Id: {Id} DayId: {DayOfWeekId} Interval {Start} - {Stop} ";
        }
    }
}