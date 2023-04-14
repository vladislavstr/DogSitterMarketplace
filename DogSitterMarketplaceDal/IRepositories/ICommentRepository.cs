using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface ICommentRepository
    {
        public List<CommentEntity> GetAllComments();

        public CommentEntity GetCommentById(int id);

        public void DeleteCommentById(int id);

        public CommentEntity AddComment(CommentEntity addComment);

        public CommentEntity UpdateComment(CommentEntity comment);

        public UserEntity GetUserById(int id);
    }
}
