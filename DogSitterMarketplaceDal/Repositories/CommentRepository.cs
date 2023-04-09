using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DogSitterMarketplaceDal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger<ICommentRepository> _logger;

        public CommentRepository(ILogger<ICommentRepository> logger)
        {
            _context = new OrdersAndPetsAndCommentsContext();
            _logger = logger;
        }

        public List<CommentEntity> GetAllComments()
        {
            return _context.Comments
        .Include(c => c.Order)
        .Include(c => c.CommentFromUser)
        .Include(c => c.CommentToUser)
        .Where(c => !c.IsDeleted && !c.Order.IsDeleted).ToList();
        }
    }
}
