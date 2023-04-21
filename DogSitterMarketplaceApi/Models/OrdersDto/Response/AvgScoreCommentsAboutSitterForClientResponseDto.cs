using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentsAboutSitterForClientResponseDto
    {
        public decimal AverageScore { get; set; }

        public List<CommentAboutSitterForClientResponseDto> CommentsAboutSitterForClient { get; set; } = new();
    }
}
