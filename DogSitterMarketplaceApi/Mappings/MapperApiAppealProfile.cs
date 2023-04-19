using AutoMapper;

using DogSitterMarketplaceApi.Models.AppealsDto.Response;
using DogSitterMarketplaceApi.Models.AppealsDto.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Appeals.Request;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiAppealProfile : Profile
    {
        public MapperApiAppealProfile() 
        {
            CreateMap<AppealResponse, AppealResponseDto>();
            CreateMap<AppealRequestDto, AppealRequest>();
            CreateMap<AppealStatusResponse, AppealStatusResponseDto>();
            CreateMap<AppealStatusRequestDto, AppealStatusRequest>();
            CreateMap<AppealTypeResponse, AppealTypeResponseDto>();
            CreateMap<AppealTypeRequestDto, AppealTypeRequest>();
        }
    }
}
