using AutoMapper;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllWorkProfile : Profile
    {
        public MapperBllWorkProfile()
        {
            CreateMap<DayOfWeekEntity, DayOfWeekResponse>();
            CreateMap<DayOfWeekRequest, DayOfWeekEntity>();
            CreateMap<LocationEntity, LocationResponse>();
            CreateMap<LocationRequest, LocationEntity>();
            CreateMap<LocationWorkEntity, LocationWorkResponse>();
            CreateMap<LocationWorkBaseRequest, LocationWorkEntity>();
            CreateMap<LocationWorkUpdateRequest, LocationWorkEntity>();
            CreateMap<SitterWorkRequest, SitterWorkEntity>();
            CreateMap<SitterWorkEntity, SitterWorkBaseResponse>();
            CreateMap<SitterWorkEntity, SitterWorkResponse>();
            CreateMap<TimingLocationWorkEntity, TimingLocationWorkResponse>();
            CreateMap<TimingLocationWorkRequest, TimingLocationWorkEntity>();
            CreateMap<LocationWorkEntity, LocationWorkBaseResponse>();
            CreateMap<TimingLocationWorkWithIdRequest, TimingLocationWorkEntity>();
            CreateMap<SitterWorkBaseRequest, SitterWorkEntity>();
        }
    }
}
