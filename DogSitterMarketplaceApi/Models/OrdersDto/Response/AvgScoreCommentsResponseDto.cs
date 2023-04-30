namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentsResponseDto
    {
        public decimal AverageScore { get; set; }

        public List<CommentResponseDto> Comments { get; set; } = new();
    }
}
