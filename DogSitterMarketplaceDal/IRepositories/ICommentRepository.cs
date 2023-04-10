using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface ICommentRepository
    {
        public List<CommentEntity> GetAllComments();

        public CommentEntity GetCommentById(int id);

        public void DeleteCommentById(int id);
    }
}
