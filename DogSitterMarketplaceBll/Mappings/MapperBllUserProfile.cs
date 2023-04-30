using AutoMapper;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllUserProfile : Profile
    {
        public MapperBllUserProfile()
        {
            CreateMap<UserEntity, UserResponse>();
            CreateMap<UserRoleEntity, UserRoleResponse>();
            CreateMap<UserPassportDataEntity, UserPassportDataResponse>();
            CreateMap<UserStatusEntity, UserStatusResponse>();
            CreateMap<WorkTypeEntity, WorkTypeResponse>();

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