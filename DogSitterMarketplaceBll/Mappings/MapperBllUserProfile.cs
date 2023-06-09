﻿using AutoMapper;
using DogSitterMarketplaceBll.Models.Users.Request;
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
            //User 
            CreateMap<UserEntity, UserResponse>();
            CreateMap<UserRequest, UserEntity>();

            //PassportData 
            CreateMap<UserPassportDataEntity, UserPassportDataResponse>();
            CreateMap<UserPassportDataRequest, UserPassportDataEntity>();

            //Role 
            CreateMap<UserRoleEntity, UserRoleResponse>();
            CreateMap<UserRoleRequest, UserRoleEntity>();

            //Status 
            CreateMap<UserStatusEntity, UserStatusResponse>();
            CreateMap<UserStatusRequest, UserStatusEntity>();

            //All 
            CreateMap<WorkTypeEntity, WorkTypeResponse>();
        }
    }
}