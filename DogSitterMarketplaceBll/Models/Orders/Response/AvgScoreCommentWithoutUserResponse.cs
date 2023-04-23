namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentWithoutUserResponse
    {
        public decimal AverageScore { get; set; }

        public List<CommentWithoutUserResponse> CommentsWithoutUser { get; set; } = new();
    }
}
