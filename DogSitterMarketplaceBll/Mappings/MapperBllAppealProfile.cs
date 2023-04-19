using AutoMapper;

using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Appeals.Request;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllAppealProfile : Profile
    {
        public MapperBllAppealProfile()
        {
            CreateMap<AppealEntity, AppealResponse>();
            CreateMap<AppealStatusEntity, AppealStatusResponse>();
            //CreateMap<AppealStatusRequest, AppealStatusEntity();
            CreateMap<AppealTypeEntity, AppealTypeResponse>();
        }
    }
}
