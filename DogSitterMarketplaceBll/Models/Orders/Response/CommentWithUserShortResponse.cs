using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class CommentWithUserShortResponse : CommentResponse
    {
        public UserShortResponse CommentFromUser { get; set; }
    }
}
