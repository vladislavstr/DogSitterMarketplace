using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentAboutClientForSitterResponseDto
    {
        public double AverageScore { get; set; }

        public List<CommentAboutClientsForSitterResponseDto> CommentsAboutClientForSitter { get; set; } = new();
    }
}
