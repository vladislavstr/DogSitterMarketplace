using AutoMapper;
using AutoMapper;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore.Design;

namespace DogSitterMarketplaceBll
{
    public class MapperBllProfile : Profile
    {
        public MapperBllProfile()
        {
            CreateMap<OrderCreateRequest, OrderEntity>()
                 .ForMember(dest => dest.OrderStatus, opt => opt.Ignore())
                 .ForMember(dest => dest.SitterWork, opt => opt.Ignore())
                 .ForMember(dest => dest.Location, opt => opt.Ignore())
                 .ForMember(dest => dest.Pets, opt => opt.Ignore());
            CreateMap<OrderEntity, OrderResponse>();
            CreateMap<PetEntity, PetResponse>().ReverseMap();
            CreateMap<OrderStatusEntity, OrderStatusResponse>();
            CreateMap<SitterWorkEntity, SitterWorkResponse>();
            CreateMap<LocationEntity, LocationResponse>();
            CreateMap<CommentEntity, CommentResponse>();
            CreateMap<AppealEntity, AppealResponse>();
        }
    }
}
