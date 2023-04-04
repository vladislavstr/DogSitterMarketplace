using System.ComponentModel.DataAnnotations;
using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public UserPassportDataEntity? PassportData { get; set; }

        public UserRoleEntity Role { get; set; }

        public UserStatusEntity Status { get; set; }

        public ICollection<PetEntity>? Pets { get; set; }
    }
}
