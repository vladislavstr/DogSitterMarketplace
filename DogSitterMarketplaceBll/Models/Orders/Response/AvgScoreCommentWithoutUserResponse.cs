namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentWithoutUserResponse
    {
        public double AverageScore { get; set; }

        public List<CommentWithoutUserResponse> CommentsWithoutUser { get; set; } = new();
    }
}
