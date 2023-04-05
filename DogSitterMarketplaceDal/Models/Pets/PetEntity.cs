using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;


namespace DogSitterMarketplaceDal.Models.Pets
{
    public class PetEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string Characteristics { get; set; }

        [Required]
        public AnimalTypeEntity Type { get; set; }

        [Required]
        public UserEntity User { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
