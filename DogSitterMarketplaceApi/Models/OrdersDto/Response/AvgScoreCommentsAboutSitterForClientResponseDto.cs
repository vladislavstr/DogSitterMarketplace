using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentsAboutSitterForClientResponseDto
    {
        public double AverageScore { get; set; }

        public List<CommentAboutSitterForClientResponseDto> CommentsAboutSitterForClient { get; set; } = new();
    }
}
