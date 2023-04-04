using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;


namespace DogSitterMarketplaceDal.Models.Pets
{
    public class PetEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Characteristics { get; set; }

        [Required]
        public AnimalTypeEntity Type { get; set; }

        [Required]
        public UserEntity User { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
