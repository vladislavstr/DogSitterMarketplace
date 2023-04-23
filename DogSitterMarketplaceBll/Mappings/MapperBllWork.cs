﻿using AutoMapper;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBllProfile.Models.Works.Request;
using DogSitterMarketplaceBllProfile.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllWorkProfile : Profile
    {
        public MapperBllWorkProfile()
        {
            CreateMap<DayOfWeekRequest, DayOfWeekEntity>();
            CreateMap<DayOfWeekEntity, DayOfWeekResponse>();
            CreateMap<LocationRequest, LocationEntity>();
            CreateMap<LocationEntity, LocationResponse>();
            CreateMap<LocationWorkRequest, LocationWorkEntity>();
            CreateMap<LocationWorkEntity, LocationWorkResponse>();
            CreateMap<SitterWorkRequest, SitterWorkEntity>();
            CreateMap<SitterWorkEntity, SitterWorkResponse>();
            CreateMap<TimingLocationWorkRequest, TimingLocationWorkEntity>();
            CreateMap<TimingLocationWorkEntity, TimingLocationWorkResponse>();
            CreateMap<TimingLocationWorkWithIdRequest, TimingLocationWorkEntity>();
        }
    }
}
