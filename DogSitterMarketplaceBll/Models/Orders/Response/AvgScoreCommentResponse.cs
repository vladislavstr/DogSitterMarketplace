
namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentResponse
    {
        public decimal AverageScore { get; set; }

        public List<CommentResponse> Comments { get; set; } = new();
    }
}
