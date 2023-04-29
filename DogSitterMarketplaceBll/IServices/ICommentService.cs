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

        public AvgScoreCommentsResponse<T> GetCommentsAndScoresForUserAboutHim<T>(int userId, string role) where T : CommentResponse;

        public AvgScoreCommentsResponse<T> GetCommentsAndScoresAboutOtherUsers<T>(int userIdGetComment, string roleUserGetComment,
                                                                                  int userIdToComment, string roleUserToComment) where T : CommentResponse;
    }
}
