using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceApi
{
    public class MapperApiProfile : Profile
    {
        public MapperApiProfile()
        {
            CreateMap<OrderRequestDto, OrderRequest>();
            CreateMap<OrderResponse, OrderResponseDto>();
        }
    }
}
