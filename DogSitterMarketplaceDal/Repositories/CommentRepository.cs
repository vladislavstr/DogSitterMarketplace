using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        public CommentRepository()
        {
            _context = new OrdersAndPetsAndCommentsContext();
        }

        public List<CommentEntity> GetAllComments()
        { 
        return _context.Comments
                .Include(c => c.Order)
                .Where(c => !c.IsDeleted && !c.Order.IsDeleted).ToList();
        }
    }
}
