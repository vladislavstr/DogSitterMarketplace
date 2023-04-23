using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiCommentProfile : Profile
    {
        public MapperApiCommentProfile() 
        {
            CreateMap<CommentOrderResponse, CommentOrderResponseDto>();
            CreateMap<CommentRequestDto, CommentRequest>();
            CreateMap<CommentUpdateDto, CommentUpdate>();
            CreateMap<CommentAboutSitterForClientResponse, CommentAboutSitterForClientResponseDto>();
            CreateMap<CommentAboutClientsForSitterResponse, CommentAboutClientsForSitterResponseDto>();
            CreateMap<CommentWithoutUserResponse, CommentWithoutUserResponseDto>();
            CreateMap<UserForCommentResponse, UserForCommentResponseDto>();
            CreateMap<AvgScoreCommentsAboutSitterForClientResponse, AvgScoreCommentsAboutSitterForClientResponseDto>();
            CreateMap<AvgScoreCommentAboutClientForSitterResponse, AvgScoreCommentAboutClientForSitterResponseDto>();
            CreateMap<AvgScoreCommentResponse, AvgScoreCommentResponseDto>();
            CreateMap<AvgScoreCommentWithoutUserResponse, AvgScoreCommentWithoutUserResponseDto>();
        }
    }
}
