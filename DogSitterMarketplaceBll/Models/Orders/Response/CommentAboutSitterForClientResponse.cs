using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class CommentAboutSitterForClientResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

      //  public double AverageScore { get; set; }

        public UserForCommentResponse CommentFromUser { get; set; }

        public UserShortResponse CommentToUser { get; set; }
    }
}
