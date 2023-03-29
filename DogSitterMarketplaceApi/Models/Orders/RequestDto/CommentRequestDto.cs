using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Orders.RequestDto
{
    public class CommentRequestDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public OrderRequestDto Order { get; set; }
        public UserRequestDto CommentFromUser { get; set; }
        public UserRequestDto CommentToUser { get; set; }
    }
}
