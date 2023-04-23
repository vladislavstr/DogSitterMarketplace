using DogSitterMarketplaceApi.Models.UsersDto.Response;

namespace DogSitterMarketplaceApi.Models.PetsDto.Response
{
    public class PetResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Characteristics { get; set; }

        public AnimalTypeResponseDto Type { get; set; }

        public UserShortResponseDto User { get; set; }
    }
}
