using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DogSitterMarketplaceDal.Models.Pets;

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

        //[Required]
        public bool IsDeleted { get; set; }

        //[Required]
        [ForeignKey(nameof(UserPassportDataId))]
        public UserPassportDataEntity UserPassportData { get; set; }

        public int UserPassportDataId { get; set; }

        //[Required]
        [ForeignKey(nameof(UserRoleId))]
        public UserRoleEntity UserRole { get; set; }

        public int UserRoleId { get; set; }

        //[Required]
        [ForeignKey(nameof(UserStatusId))]
        public UserStatusEntity UserStatus { get; set; }

        public int UserStatusId { get; set; }

        [ForeignKey(nameof(PetsId))]
        public ICollection<PetEntity>? Pets { get; set; }

        public int? PetsId { get; set; }

    }
}
