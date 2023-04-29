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
            CreateMap<CommentsAboutOtherUsersResponse, CommentsAboutOtherUsersResponseDto>();
            CreateMap<CommentResponse, CommentWithoutUserResponseDto>();
            CreateMap<UserForCommentResponse, UserForCommentResponseDto>();
            CreateMap<AvgScoreCommentsResponse<CommentResponse>, AvgScoreCommentWithoutUserResponseDto>()
                .ForMember(dest => dest.CommentsWithoutUser, opt => opt.MapFrom(src => src.Comments));
            CreateMap<AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>, AvgScoreCommentsAboutOtherUsersResponseDto>()
                .ForMember(dest => dest.CommentsAboutOtherUsers, opt => opt.MapFrom(src => src.Comments));
            CreateMap<AvgScoreCommentsResponse<CommentWithUserShortResponse>, AvgScoreCommentsResponseDto>();
            CreateMap<AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>, AvgScoreCommentsResponseDto>();
           // CreateMap<AvgScoreCommentWithoutUserResponse, AvgScoreCommentWithoutUserResponseDto>();
        }
    }
}
