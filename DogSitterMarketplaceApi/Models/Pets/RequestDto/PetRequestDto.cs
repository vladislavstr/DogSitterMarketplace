using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Pets.RequestDto
{
    public class PetRequestDto
    {
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public AnimalTypeRequestDto AnimalType { get; set; }
        public UserRequestDto User { get; set; }
    }
}
