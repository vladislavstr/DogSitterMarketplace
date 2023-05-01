using AutoMapper;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllAppealProfile : Profile
    {
        public MapperBllAppealProfile()
        {
            //Appeal
            CreateMap<AppealEntity, AppealResponse>();
            CreateMap<AppealRequest, AppealEntity>();
            //AppealStatus
            CreateMap<AppealStatusEntity, AppealStatusResponse>();
            CreateMap<AppealStatusRequest, AppealStatusEntity>();
            //AppealType
            CreateMap<AppealTypeEntity, AppealTypeResponse>();
            CreateMap<AppealTypeRequest, AppealTypeEntity>();
            //AppealUpdate
            CreateMap<AppealUpdate, AppealEntity>().ReverseMap();
        }
    }
}
