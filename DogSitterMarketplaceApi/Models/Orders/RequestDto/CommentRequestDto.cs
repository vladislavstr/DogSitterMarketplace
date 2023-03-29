using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Orders.RequestDto
{
    public class CommentRequestDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public int OrderId { get; set; }
        public int CommentFromUserId { get; set; }
        public int CommentToUserId { get; set; }
    }
}
