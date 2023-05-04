using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)"),]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)"),]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)"),]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)"),]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [AllowNull]
        [ForeignKey(nameof(UserPassportDataId))]
        public UserPassportDataEntity? UserPassportData { get; set; }

        [AllowNull]
        public int? UserPassportDataId { get; set; }

        [ForeignKey(nameof(UserRoleId))]
        public UserRoleEntity UserRole { get; set; }

        public int UserRoleId { get; set; }

        [ForeignKey(nameof(UserStatusId))]
        public UserStatusEntity UserStatus { get; set; }

        public int UserStatusId { get; set; }

        public ICollection<PetEntity>? Pets { get; set; }

        public List<SitterWorkEntity> SitterWorks { get; set; } = new List<SitterWorkEntity>();
    }
}
