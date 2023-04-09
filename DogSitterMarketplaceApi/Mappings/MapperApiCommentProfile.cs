using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiCommentProfile : Profile
    {
        public MapperApiCommentProfile() 
        {
            CreateMap<CommentOrderResponse, CommentOrderResponseDto>();
        }
    }
}
