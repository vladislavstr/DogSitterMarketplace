
namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentResponse
    {
        public double AverageScore { get; set; }

        public List<CommentResponse> Comments { get; set; } = new();
    }
}
