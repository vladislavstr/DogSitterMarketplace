namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentsAboutSitterForClientResponse
    {
        public double AverageScore { get; set; }

        public List<CommentAboutSitterForClientResponse> CommentsAboutSitterForClient { get; set; } = new();
    }
}
