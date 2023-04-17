using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class LocationWorkEntity
    {
        //public int Id { get; set; }

        //public int Price { get; set; }

        //public SitterWorkEntity SitterWork { get; set; }

        //public LocationEntity Location { get; set; }

        //public bool IsNotActive { get; set; }

        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "decimal(6,2)")]
        public decimal? Price { get; set; }

        [Required]
        [ForeignKey(nameof(LocationId))]
        public LocationEntity? Location { get; set; }

        public int? LocationId { get; set; }

        [Required]
        public ICollection<TimingLocationWorkEntity> TimingLocationWorks { get; } = new List<TimingLocationWorkEntity>();

        [Required]
        public bool? IsNotActive { get; set; }

        [Required]
        [ForeignKey(nameof(SitterWorkId))]
        public SitterWorkEntity SitterWork { get; set; }

        public int? SitterWorkId { get; set; }
    }
}