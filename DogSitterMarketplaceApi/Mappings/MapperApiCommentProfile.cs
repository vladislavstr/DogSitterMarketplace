using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiCommentProfile : Profile
    {
        public MapperApiCommentProfile() 
        {
            CreateMap<CommentOrderResponse, CommentOrderResponseDto>();
            CreateMap<CommentRequestDto, CommentRequest>();
            CreateMap<CommentUpdateDto, CommentUpdate>();
        }
    }
}
