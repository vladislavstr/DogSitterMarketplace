using AutoMapper;

using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceBll.Models.Appeals.Response;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllAppealProfile : Profile
    {
        public MapperBllAppealProfile()
        {
            CreateMap<AppealEntity, AppealResponse>();
            CreateMap<AppealStatusEntity, AppealStatusResponse>();
            CreateMap<AppealTypeEntity, AppealTypeResponse>();
        }
    }
}
