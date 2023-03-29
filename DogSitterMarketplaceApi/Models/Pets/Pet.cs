using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Pets
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public bool IsDeleted { get; set; }
        public AnimalType Type { get; set; }
        public User User { get; set; }
    }
}
