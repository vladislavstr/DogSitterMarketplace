using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class CommentsAboutOtherUsersResponse : CommentResponse
    {
        public UserForCommentResponse CommentFromUser { get; set; }
    }
}
