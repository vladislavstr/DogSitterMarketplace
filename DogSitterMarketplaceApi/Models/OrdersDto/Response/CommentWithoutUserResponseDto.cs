namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class CommentWithoutUserResponseDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        public double AverageScore { get; set; }
    }
}
