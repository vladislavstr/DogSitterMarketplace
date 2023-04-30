using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface ICommentRepository
    {
        public Task<List<CommentEntity>> GetAllComments();

        public Task<CommentEntity> GetCommentById(int id);

        public Task DeleteCommentById(int id);

        public Task<CommentEntity> AddComment(CommentEntity addComment);

        public Task<CommentEntity> UpdateComment(CommentEntity comment);

        public Task<List<CommentEntity>> GetAllCommentsAndScoresByUserId(int userIdToComment);
    }
}
