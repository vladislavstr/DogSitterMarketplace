using AutoMapper;

using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Users.Request;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllUserProfile : Profile
    {
        public MapperBllUserProfile()
        {
            CreateMap<UserEntity, UserResponse>();
            CreateMap<UserRequest, UserEntity>();
            CreateMap<UserUpdate,UserEntity>();
        }
    }
}