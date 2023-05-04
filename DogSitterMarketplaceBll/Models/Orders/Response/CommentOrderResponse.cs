using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class CommentOrderResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        public OrderResponse Order { get; set; }

        public UserShortResponse CommentFromUser { get; set; }

        public UserShortResponse CommentToUser { get; set; }
    }
}
