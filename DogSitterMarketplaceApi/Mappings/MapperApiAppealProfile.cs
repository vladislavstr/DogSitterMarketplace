using AutoMapper;
using DogSitterMarketplaceApi.Models.AppealsDto.Request;
using DogSitterMarketplaceApi.Models.AppealsDto.Response;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiAppealProfile : Profile
    {
        public MapperApiAppealProfile()
        {
            //Appeal
            CreateMap<AppealResponse, AppealResponseDto>();
            CreateMap<AppealRequestDto, AppealRequest>();
            //AppealStatus
            CreateMap<AppealStatusResponse, AppealStatusResponseDto>();
            CreateMap<AppealStatusRequestDto, AppealStatusRequest>();
            //AppealType
            CreateMap<AppealTypeResponse, AppealTypeResponseDto>();
            CreateMap<AppealTypeRequestDto, AppealTypeRequest>();
            //AppealUpdate
            CreateMap<AppealUpdateDto, AppealUpdate>();
            CreateMap<AppealResponse, AppealUpdate>();

        }
    }
}
