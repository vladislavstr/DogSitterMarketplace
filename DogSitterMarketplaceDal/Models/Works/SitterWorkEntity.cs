using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class SitterWorkEntity
    {
        //public int Id { get; set; }

        //public string? Comment { get; set; }

        //public UserEntity User { get; set; }

        //public WorkTypeEntity WorkType { get; set; }

        //public bool IsDeleted { get; set; }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(500)"),]
        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        public int? UserId { get; set; }

        [Required]
        [ForeignKey(nameof(WorkTypeId))]
        public WorkTypeEntity? WorkType { get; set; }

        public int WorkTypeId { get; set; }

        [Required]
        public ICollection<LocationWorkEntity> LocationWork { get; set; } = new List<LocationWorkEntity>();

        [Required]
        public bool IsDeleted { get; set; }
    }
}