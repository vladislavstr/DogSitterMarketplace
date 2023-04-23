using DogSitterMarketplaceDal.Models.Pets;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public UserPassportDataEntity? UserPassportData { get; set; }

        public UserRoleEntity UserRole { get; set; }

        public UserStatusEntity UserStatus { get; set; }

        public ICollection<PetEntity>? Pets { get; set; }
    }
}
