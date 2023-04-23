namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentAboutClientForSitterResponse
    {
        public decimal AverageScore { get; set; }

        public List<CommentAboutClientsForSitterResponse> CommentsAboutClientForSitter { get; set; } = new();
    }
}
