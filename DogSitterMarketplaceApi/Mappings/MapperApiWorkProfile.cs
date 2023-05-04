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
            CreateMap<LocationWorkRequestDto,LocationWorkBaseRequest>();
            CreateMap<LocationWorkRequestDto, LocationWorkUpdateRequest>();
            CreateMap<UpdateLocationWorkRequesDto, LocationWorkUpdateRequest>();
            CreateMap<LocationWorkResponse, LocationWorkResponseDto>();
            CreateMap<LocationRequestDto, LocationRequest>();
            CreateMap<LocationResponse, LocationResponseDto>();
            CreateMap<LocationResponse, LocationWorkBaseResponseDto>();
            CreateMap<SitterWorkRequestDto, SitterWorkRequest>();
            CreateMap<SitterWorkBaseRequestDto, SitterWorkBaseRequest>();
            CreateMap<SitterWorkBaseRequestDto, SitterWorkRequest>();
            CreateMap<SitterWorkUpdateRequestDto,SitterWorkRequest>();
            CreateMap<SitterWorkBaseResponse, SitterWorkBaseResponseDto>();
            CreateMap<SitterWorkResponse, SitterWorkResponseDto>();
            CreateMap<TimingLocationWorkResponse, TimingLocationWorkResponseDto>();
            CreateMap<LocationWorkBaseResponse, LocationWorkResponseDto>();
            CreateMap<LocationWorkBaseResponse, LocationWorkBaseResponseDto>();
        }

        private TimeSpan ConvertToTimeSpan(int hour, int minut)
        {
            string s = $"{hour}:{minut}:00";
            var result = TimeSpan.Parse(s);

            return result;
        }
    }
}
