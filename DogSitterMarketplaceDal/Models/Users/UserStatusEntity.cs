using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserStatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Comment { get; set; }
    }
}
