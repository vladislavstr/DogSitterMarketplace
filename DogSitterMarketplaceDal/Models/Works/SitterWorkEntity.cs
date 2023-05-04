using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class SitterWorkEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(500)"),]
        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public  UserEntity User { get; set; }

        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(WorkTypeId))]
        public  WorkTypeEntity WorkType { get; set; }

        public int WorkTypeId { get; set; }

        [Required]
        public List<LocationWorkEntity> LocationsWork { get; set; } = new List<LocationWorkEntity>();

        [Required]
        public bool IsDeleted { get; set; }
    }
}