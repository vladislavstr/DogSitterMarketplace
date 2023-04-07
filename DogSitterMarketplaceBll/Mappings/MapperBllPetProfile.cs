using AutoMapper;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllPetProfile : Profile
    {
        public MapperBllPetProfile()
        {
            CreateMap<PetEntity, PetResponse>();
            CreateMap<AnimalTypeEntity, AnimalTypeResponse>();
            CreateMap<UserEntity, UserShortResponse>();
        }
    }
}
