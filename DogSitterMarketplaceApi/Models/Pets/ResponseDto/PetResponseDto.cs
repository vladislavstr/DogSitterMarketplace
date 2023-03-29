using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Pets.ResponseDto
{
    public class PetResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public AnimalTypeResponseDto Type { get; set; }
        public UserResponseDto User { get; set; }
    }
}
