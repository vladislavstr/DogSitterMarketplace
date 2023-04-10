using DogSitterMarketplaceCore.Exceptions;
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

        public CommentEntity GetCommentById(int id)
        {
            try
            {
                return _context.Comments
                    .Include(c => c.Order)
                    .Include(c => c.Order.OrderStatus)
                    .Include(c => c.CommentFromUser)
                    .Include(c => c.CommentToUser)
                    .Single(c => c.Id == id && !c.IsDeleted
                    && !c.Order.IsDeleted
                    && !c.Order.OrderStatus.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                _logger.LogDebug($"({nameof(CommentEntity)} with id {id} not found)");
                throw new NotFoundException(id, nameof(CommentEntity));
            }
        }

        public void DeleteCommentById(int id)
        {
            try
            {
                var commentDB = _context.Comments.Single(c => c.Id == id && !c.IsDeleted);
                commentDB.IsDeleted = true;
                _context.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                _logger.LogDebug($"{nameof(CommentEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(CommentEntity));
            }
        }
    }
}
