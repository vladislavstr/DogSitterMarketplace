using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Orders.Request
{
    public class CommentRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public OrderRequest Order { get; set; }
        public UserRequest CommentFromUser { get; set; }
        public UserRequest CommentToUser { get; set; }
    }
}
