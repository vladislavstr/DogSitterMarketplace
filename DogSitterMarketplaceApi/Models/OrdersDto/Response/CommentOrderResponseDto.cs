using DogSitterMarketplaceApi.Models.UsersDto.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class CommentOrderResponseDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        //public double AverageScore { get; set; }

        public OrderResponseDto Order { get; set; }

        public UserShortResponseDto CommentFromUser { get; set; }

        public UserShortResponseDto CommentToUser { get; set; }
    }
}
