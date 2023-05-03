using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DogSitterMarketplaceDal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger _logger;

        public CommentRepository(OrdersAndPetsAndCommentsContext context, ILogger nLogger)
        {
            _context = context;
            _logger = nLogger;
        }

        public async Task<List<CommentEntity>> GetAllCommentsAndScoresByUserId(int userIdToComment)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userIdToComment);

            if (user == null)
            {
                _logger.Log(LogLevel.Debug, $"({nameof(CommentRepository)} {nameof(GetAllCommentsAndScoresByUserId)} {nameof(CommentEntity)} with CommentUserTo with id {userIdToComment} not found)");
                throw new NotFoundException(userIdToComment, nameof(UserEntity));
            }

            return await _context.Comments
                        .Include(c => c.Order)
                        .ThenInclude(o => o.SitterWork)
                        .Include(c => c.Order)
                        .ThenInclude(o => o.OrderStatus)
                        .Include(c => c.CommentFromUser)
                        .ThenInclude(u => u.UserRole)
                        .Include(c => c.CommentToUser)
                        .ThenInclude(u => u.UserRole)
                        .Where(c => c.CommentToUserId == userIdToComment).ToListAsync();
        }

        public async Task<List<CommentEntity>> GetAllComments()
        {
            return await _context.Comments
                        .Include(c => c.Order)
                        .ThenInclude(o => o.SitterWork)
                        .Include(c => c.Order)
                        .ThenInclude(o => o.OrderStatus)
                        .Include(c => c.CommentFromUser)
                        .Include(c => c.CommentToUser).ToListAsync();
        }

        public async Task<CommentEntity> GetCommentById(int id)
        {
            try
            {
                return await _context.Comments
                           .Include(c => c.Order)
                           .Include(c => c.Order.OrderStatus)
                           .Include(c => c.CommentFromUser)
                           .Include(c => c.CommentToUser)
                           .SingleAsync(c => c.Id == id);
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"({nameof(CommentRepository)} {nameof(GetCommentById)} {nameof(CommentEntity)} with id {id} not found)");
                throw new NotFoundException(id, nameof(CommentEntity));
            }
        }

        public async Task DeleteCommentById(int id)
        {
            try
            {
                var commentDB = await _context.Comments.SingleAsync(c => c.Id == id && !c.IsDeleted);
                commentDB.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"({nameof(CommentRepository)} {nameof(DeleteCommentById)} {nameof(CommentEntity)} with id {id} not found)");
                throw new NotFoundException(id, nameof(CommentEntity));
            }
        }

        public async Task<CommentEntity> AddComment(CommentEntity addComment)
        {
            try
            {
                await _context.Comments.AddAsync(addComment);
                await _context.SaveChangesAsync();

                return await _context.Comments
                                .SingleAsync(c => c.Id == addComment.Id);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, $"({ex}, {nameof(CommentRepository)} {nameof(AddComment)} {nameof(CommentEntity)}");
                throw new ArgumentException();
            }
        }

        public async Task<CommentEntity> UpdateComment(CommentEntity comment)
        {
            var commentDB = await _context.Comments.SingleOrDefaultAsync(c => c.Id == comment.Id && !c.IsDeleted);

            if (commentDB == null)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentRepository)} {nameof(UpdateComment)} {(nameof(CommentEntity))} with id {comment.Id} not found");
                throw new NotFoundException(comment.Id, nameof(CommentEntity));
            }

            commentDB.Text = comment.Text;
            commentDB.Score = comment.Score;
            commentDB.OrderId = comment.OrderId;
            commentDB.CommentFromUserId = comment.CommentFromUserId;
            commentDB.CommentToUserId = comment.CommentToUserId;
            await _context.SaveChangesAsync();

            return commentDB;
        }
    }
}
