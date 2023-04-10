using DogSitterMarketplaceApi.Models.UsersDto.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class CommentResponseDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        public UserShortResponseDto CommentFromUser { get; set; }

        public UserShortResponseDto CommentToUser { get; set; }
    }
}
