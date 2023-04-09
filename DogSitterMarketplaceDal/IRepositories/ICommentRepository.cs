using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface ICommentRepository
    {
        public List<CommentEntity> GetAllComments();

    }
}
