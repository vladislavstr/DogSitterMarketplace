using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class LocationWorkEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(LocationId))]
        public LocationEntity Location { get; set; }

        public int? LocationId { get; set; }

        [Required]
        public List<TimingLocationWorkEntity> TimingLocationWorks { get; } = new List<TimingLocationWorkEntity>();

        [Required]
        public bool IsNotActive { get; set; }

        [Required]
        [ForeignKey(nameof(SitterWorkId))]
        public SitterWorkEntity SitterWork { get; set; }

        public int SitterWorkId { get; set; }
    }
}