namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class AvgScoreCommentsAboutOtherUsersResponseDto
    {
        public decimal AverageScore { get; set; }

        public List<CommentsAboutOtherUsersResponseDto> CommentsAboutOtherUsers { get; set; } = new();
    }
}
