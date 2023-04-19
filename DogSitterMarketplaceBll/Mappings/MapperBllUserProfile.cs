using AutoMapper;

using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllUserProfile : Profile
    {
        public MapperBllUserProfile()
        {
            CreateMap<UserEntity,UserResponse>();
            CreateMap<UserRoleEntity,UserRoleResponse>();
            CreateMap<UserPassportDataEntity,UserPassportDataResponse>();
            CreateMap<UserStatusEntity,UserStatusResponse>();

            //CreateMap<UserEntity, UserResponse>();
            //CreateMap<UserRequest, UserEntity>();
            //CreateMap<UserPassportDataRequest, UserPassportDataEntity>();
            //CreateMap<UserPassportDataEntity, UserPassportDataResponse>();
            //CreateMap<UserRoleRequest, UserRoleEntity>();
            //CreateMap<UserRoleEntity, UserRoleResponse>();
            //CreateMap<UserStatusRequest, UserStatusEntity>();
            //CreateMap<UserStatusEntity, UserStatusResponse>();
            //CreateMap<PetRequest, PetEntity>();
            //CreateMap<UserUpdate, UserEntity>();
        }
    }
}