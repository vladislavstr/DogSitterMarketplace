using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentResponseDto
    {
        public decimal AverageScore { get; set; }

        public List<CommentResponseDto> Comments { get; set; } = new();
    }
}
