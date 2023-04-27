using AutoMapper;
using DogSitterMarketplaceApi.Models.Works.Request;
using DogSitterMarketplaceApi.Models.WorksDto;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiWorkProfile : Profile
    {
        public MapperApiWorkProfile()
        {
            CreateMap<TimingLocationWorkRequestDto, TimingLocationWorkRequest>()
                 .ForMember(lw => lw.Start, lwd => lwd.MapFrom(q => ConvertToTimeSpan(q.StartHour, q.StartMinut)))
                 .ForMember(lw => lw.Stop, lwd => lwd.MapFrom(q => ConvertToTimeSpan(q.StopHour, q.StopMinut)));
            CreateMap<TimingLocationWorkWithIdRequesDto, TimingLocationWorkWithIdRequest>()
                 .ForMember(lw => lw.Start, lwd => lwd.MapFrom(q => ConvertToTimeSpan(q.StartHour, q.StartMinut)))
                 .ForMember(lw => lw.Stop, lwd => lwd.MapFrom(q => ConvertToTimeSpan(q.StopHour, q.StopMinut)));
            CreateMap<TimingLocationWorkWithIdRequesDto, TimingLocationWorkRequestDto>();
            CreateMap<TimingLocationWorkResponse, TimingLocationWorkResponseDto>();
            CreateMap<DayOfWeekResponse, DayOfWeekResponseDto>();
            CreateMap<DayOfWeekRequestDto, DayOfWeekRequest>();
            CreateMap<LocationWorkRequestDto, LocationWorkRequest>();
            CreateMap<UpdateLocationWorkRequesDto, UpdateLocationWorkRequest>();
            CreateMap<LocationWorkResponse, LocationWorkResponseDto>();
            CreateMap<LocationRequestDto, LocationRequest>();
            CreateMap<LocationResponse, LocationResponseDto>();
            CreateMap<SitterWorkRequestDto, SitterWorkRequest>();
            CreateMap<SitterWorkResponse, SitterWorkResponseDto>();
            CreateMap<TimingLocationWorkResponse, TimingLocationWorkResponseDto>();
        }

        private TimeSpan ConvertToTimeSpan(int hour, int minut)
        {
            string s = $"{hour}:{minut}:00";
            var result = TimeSpan.Parse(s);

            return result;
        }
    }
}
