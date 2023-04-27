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
            //CreateMap<AppealResponse, AppealUpdateDto>().ReverseMap();
            //CreateMap<AppealUpdate, AppealUpdateDto>().ReverseMap();
            CreateMap<AppealUpdateDto, AppealUpdate>();
            CreateMap<AppealResponse, AppealUpdate>();

        }
    }
}
