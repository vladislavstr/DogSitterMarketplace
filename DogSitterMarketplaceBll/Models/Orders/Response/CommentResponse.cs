using DogSitterMarketplaceBll.Models.Users;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class CommentResponse
    {
        public string Text { get; set; }
        public int Score { get; set; }
        public OrderResponse Order { get; set; }
        public UserResponse CommentFromUser { get; set; }
        public UserResponse CommentToUser { get; set; }
    }
}
