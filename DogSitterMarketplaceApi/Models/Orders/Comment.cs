using DogSitterMarketplaceApi.Models.Orders;
using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Orders
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public bool IsDeleted { get; set; }
        public Order Order { get; set; }
        public User CommentFromUser { get; set; }
        public User CommentToUser { get; set; }
    }
}
