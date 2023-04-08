using DogSitterMarketplaceDal.IRepositories;

namespace DogSitterMarketplaceDal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        public CommentRepository()
        {
            _context = new OrdersAndPetsAndCommentsContext();
        }
    }
}
