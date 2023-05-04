using AutoMapper;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllOrderProfile : Profile
    {
        public MapperBllOrderProfile()
        {
            CreateMap<OrderCreateRequest, OrderEntity>()
                 .ForMember(dest => dest.OrderStatus, opt => opt.Ignore())
                 .ForMember(dest => dest.SitterWork, opt => opt.Ignore())
                 .ForMember(dest => dest.Location, opt => opt.Ignore())
                 .ForMember(dest => dest.Pets, opt => opt.Ignore());
            CreateMap<OrderEntity, OrderResponse>();
            CreateMap<OrderStatusEntity, OrderStatusResponse>();
            CreateMap<SitterWorkEntity, SitterWorkResponse>();
            CreateMap<SitterWorkEntity, SitterWorkBaseResponse>();
            CreateMap<WorkTypeEntity, WorkTypeResponse>();
            CreateMap<LocationEntity, LocationResponse>();
            CreateMap<CommentEntity, CommentWithUserShortResponse>();
            CreateMap<AppealEntity, AppealResponse>();
            CreateMap<AppealTypeEntity, AppealTypeResponse>();
            CreateMap<AppealStatusEntity, AppealStatusResponse>();
            CreateMap<OrderUpdate, OrderEntity>()
                 .ForMember(dest => dest.OrderStatus, opt => opt.Ignore())
                 .ForMember(dest => dest.SitterWork, opt => opt.Ignore())
                 .ForMember(dest => dest.Location, opt => opt.Ignore())
                 .ForMember(dest => dest.Comments, opt => opt.Ignore())
                 .ForMember(dest => dest.Appeals, opt => opt.Ignore())
                 .ForMember(dest => dest.Pets, opt => opt.Ignore());
        }
    }
}
