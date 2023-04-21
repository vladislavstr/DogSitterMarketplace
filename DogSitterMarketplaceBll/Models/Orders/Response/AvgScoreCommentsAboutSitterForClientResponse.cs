namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentsAboutSitterForClientResponse
    {
        public decimal AverageScore { get; set; }

        public List<CommentAboutSitterForClientResponse> CommentsAboutSitterForClient { get; set; } = new();
    }
}
