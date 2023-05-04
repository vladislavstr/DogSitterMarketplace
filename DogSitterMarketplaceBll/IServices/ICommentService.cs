using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ICommentService
    {
        public Task<List<CommentOrderResponse>> GetAllNotDeletedComments();

        public Task<CommentOrderResponse> GetNotDeletedCommentById(int id);

        public Task DeleteCommentById(int id);

        public Task<CommentOrderResponse> AddComment(CommentRequest addComment);

        public Task<CommentOrderResponse> UpdateComment(CommentUpdate commentUpdate);

        public Task<AvgScoreCommentsResponse<T>> GetCommentsAndScoresForUserAboutHim<T>(int userId, string role) where T : CommentResponse;

        public Task<AvgScoreCommentsResponse<T>> GetCommentsAndScoresAboutOtherUsers<T>(int userIdGetComment, string roleUserGetComment,
                                                                                  int userIdToComment, string roleUserToComment) where T : CommentResponse;
    }
}
