using AutoMapper;
using DogSitterMarketplaceApi.Models.PetsDto.Request;
using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiPetProfile : Profile
    {
        public MapperApiPetProfile()
        {
            CreateMap<PetResponse, PetResponseDto>();
            CreateMap<AnimalTypeResponse, AnimalTypeResponseDto>();
            CreateMap<UserShortResponse, UserShortResponseDto>();
            CreateMap<PetRequestDto, PetRequest>();
        }
    }
}
