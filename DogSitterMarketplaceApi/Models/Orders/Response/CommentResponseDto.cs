using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Orders.Response
{
    public class CommentResponseDto
    {
        public string Text { get; set; }
        public int Score { get; set; }
        public OrderResponseDto Order { get; set; }
        public UserResponseDto CommentFromUser { get; set; }
        public UserResponseDto CommentToUser { get; set; }
    }
}
