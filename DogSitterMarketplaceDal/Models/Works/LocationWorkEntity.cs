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
        public SitterWorkEntity SitterWork { get; set; }

        [Required]
        public LocationEntity Location { get; set; }

        [Column(TypeName = "bit")]
        public bool IsNotActive { get; set; }
    }
}