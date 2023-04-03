using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserRoleEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
