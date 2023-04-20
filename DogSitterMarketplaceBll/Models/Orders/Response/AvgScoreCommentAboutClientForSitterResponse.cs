namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentAboutClientForSitterResponse
    {
        public double AverageScore { get; set; }

        public List<CommentAboutClientsForSitterResponse> CommentsAboutClientForSitter { get; set; } = new();
    }
}
