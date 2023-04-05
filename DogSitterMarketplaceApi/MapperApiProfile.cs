using AutoMapper;
using DogSitterMarketplaceApi.Models.AppealsDto.Response;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceApi
{
    public class MapperApiProfile : Profile
    {
        public MapperApiProfile()
        {
            CreateMap<OrderCreateRequestDto, OrderCreateRequest>();
            CreateMap<OrderResponse, OrderResponseDto>();
            CreateMap<OrderStatusResponse, OrderStatusResponseDto>();
            CreateMap<SitterWorkResponse, SitterWorkResponseDto>();
            CreateMap<LocationResponse, LocationResponseDto>();
            CreateMap<OrderResponse, OrderResponseDto>();
            CreateMap<PetResponse, PetResponseDto>();
            CreateMap<CommentResponse, CommentResponseDto>();
            CreateMap<AppealResponse, AppealResponseDto>();
        }
    }
}
