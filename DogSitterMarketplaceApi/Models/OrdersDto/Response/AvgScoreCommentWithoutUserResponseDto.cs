using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentWithoutUserResponseDto
    {
        public decimal AverageScore { get; set; }

        public List<CommentWithoutUserResponseDto> CommentsWithoutUser { get; set; } = new();
    }
}
