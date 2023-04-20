using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentResponseDto
    {
        public double AverageScore { get; set; }

        public List<CommentResponseDto> Comments { get; set; } = new();
    }
}
