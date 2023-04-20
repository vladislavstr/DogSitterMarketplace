using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ICommentService
    {
        public List<CommentOrderResponse> GetAllNotDeletedComments();

        public CommentOrderResponse GetNotDeletedCommentById(int id);

        public void DeleteCommentById(int id);

        public CommentOrderResponse AddComment(CommentRequest commentRequest);

        public CommentOrderResponse UpdateComment(CommentUpdate commentRequest);

        public AvgScoreCommentsAboutSitterForClientResponse GetCommentsAndScoresForClientAboutSitter(int userIdGetComment, int userIdToComment);

        public AvgScoreCommentAboutClientForSitterResponse GetCommentsAndScoresForSitterAboutClient(int userIdGetComment, int userIdToComment);

        public AvgScoreCommentResponse GetCommentsAndScoresForClientAboutHim(int userId);

        public AvgScoreCommentWithoutUserResponse GetCommentsAndScoresForSitterAboutHim(int userId);
    }
}
