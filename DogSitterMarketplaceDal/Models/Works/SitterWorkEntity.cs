using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class SitterWorkEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(500)"),]
        public string? Comment { get; set; }

        [Required]
        public UserEntity User { get; set; }

        [Required]
        public WorkTypeEntity WorkType { get; set; }

        public ICollection<TimingLocationWorkEntity> TimingLocations { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }
    }
}