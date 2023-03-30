using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.Models.Pets
{
    public class PetEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Characteristics { get; set; }

        public AnimalTypeEntity Type { get; set; }

        public UserEntity User { get; set; }

        public bool IsDeleted { get; set; }
    }
}
