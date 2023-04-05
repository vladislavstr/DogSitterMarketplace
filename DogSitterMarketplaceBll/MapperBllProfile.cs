using AutoMapper;
using AutoMapper;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore.Design;

namespace DogSitterMarketplaceBll
{
    public class MapperBllProfile : Profile
    {
        public MapperBllProfile()
        {
            CreateMap<OrderRequest, OrderEntity>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(source => new OrderStatusEntity { Id = source.OrderStatusId }))
                .ForMember(dest => dest.SitterWork, opt => opt.MapFrom(source => new SitterWorkEntity { Id = source.SitterWorkId }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(source => new LocationEntity { Id = source.LocationId}));
            CreateMap<OrderEntity, OrderResponse>();
        }
    }
}
