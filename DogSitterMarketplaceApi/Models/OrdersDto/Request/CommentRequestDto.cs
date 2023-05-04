
namespace DogSitterMarketplaceApi.Models.OrdersDto.Request
{
    public class CommentRequestDto
    {
        public string Text { get; set; }

        public int Score { get; set; }

        public int OrderId { get; set; }

        public int CommentFromUserId { get; set; }

        public int CommentToUserId { get; set; }
    }
}
