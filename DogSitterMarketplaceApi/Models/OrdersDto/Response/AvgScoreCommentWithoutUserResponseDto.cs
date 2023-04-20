using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentWithoutUserResponseDto
    {
        public double AverageScore { get; set; }

        public List<CommentWithoutUserResponseDto> CommentsWithoutUser { get; set; } = new();
    }
}
