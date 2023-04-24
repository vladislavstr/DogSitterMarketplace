using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
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

        public List<CommentEntity> GetAllCommentsAndScoresByUserId(int userIdToComment)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userIdToComment);

            if (user == null)
            {
                _logger.Log(LogLevel.Debug, $"({nameof(CommentRepository)} {nameof(GetAllCommentsAndScoresByUserId)} {nameof(CommentEntity)} with CommentUserTo with id {userIdToComment} not found)");
                throw new NotFoundException(userIdToComment, nameof(UserEntity));
            }

            return _context.Comments
                .Include(c => c.Order)
                .Include(c => c.CommentFromUser)
                .Include(c => c.CommentToUser)
                .Where(c => c.CommentToUserId == userIdToComment).ToList();
        }

        public List<CommentEntity> GetAllComments()
        {
        //    return _context.Comments
        //.Include(c => c.Order)
        //.Include(c => c.CommentFromUser)
        //.Include(c => c.CommentToUser)
        //.Where(c => !c.IsDeleted && !c.Order.IsDeleted).ToList();

            return _context.Comments
                    .Include(c => c.Order)
                    .Include(c => c.CommentFromUser)
                    .Include(c => c.CommentToUser).ToList();
        }

        public CommentEntity GetCommentById(int id)
        {
            try
            {
                //return _context.Comments
                //    .Include(c => c.Order)
                //    .Include(c => c.Order.OrderStatus)
                //    .Include(c => c.CommentFromUser)
                //    .Include(c => c.CommentToUser)
                //    .Single(c => c.Id == id && !c.IsDeleted
                //    && !c.Order.IsDeleted
                //    && !c.Order.OrderStatus.IsDeleted);

                return _context.Comments
                   .Include(c => c.Order)
                   .Include(c => c.Order.OrderStatus)
                   .Include(c => c.CommentFromUser)
                   .Include(c => c.CommentToUser)
                   .Single(c => c.Id == id);
            }
            catch (InvalidOperationException)
            {
                // _logger.LogDebug($"({nameof(CommentEntity)} with id {id} not found)");
                _logger.Log(LogLevel.Debug, $"({nameof(CommentRepository)} {nameof(GetCommentById)} {nameof(CommentEntity)} with id {id} not found)");
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
                // _logger.LogDebug($"{nameof(CommentEntity)} with id {id} not found");
                _logger.Log(LogLevel.Debug, $"({nameof(CommentRepository)} {nameof(DeleteCommentById)} {nameof(CommentEntity)} with id {id} not found)");
                throw new NotFoundException(id, nameof(CommentEntity));
            }
        }

        public CommentEntity AddComment(CommentEntity addComment)
        {
            try
            {
                _context.Comments.Add(addComment);
                _context.SaveChanges();

                return _context.Comments
                    .Include(c => c.Order)
                    .Include(c => c.CommentFromUser)
                    .Include(c => c.CommentToUser)
                    .Single(c => c.Id == addComment.Id);
            }
            catch (Exception ex)
            {
                //_logger.LogDebug($"{ex}, {nameof(CommentRepository)} {nameof(CommentEntity)} {nameof(AddComment)}");
                _logger.Log(LogLevel.Debug, $"({ex}, {nameof(CommentRepository)} {nameof(AddComment)} {nameof(CommentEntity)}");
                throw new ArgumentException();
            }
        }

        public CommentEntity UpdateComment(CommentEntity comment)
        {
            var commentDB = _context.Comments.SingleOrDefault(c => c.Id == comment.Id && !c.IsDeleted);

            if (commentDB == null)
            {
                //_logger.LogDebug($"{nameof(CommentRepository)} {nameof(UpdateComment)} {(nameof(CommentEntity))} with id {comment.Id} not found");
                _logger.Log(LogLevel.Debug, $"{nameof(CommentRepository)} {nameof(UpdateComment)} {(nameof(CommentEntity))} with id {comment.Id} not found");
                throw new NotFoundException(comment.Id, nameof(CommentEntity));
            }

            commentDB.Text = comment.Text;
            commentDB.Score = comment.Score;
            commentDB.OrderId = comment.OrderId;
            commentDB.CommentFromUserId = comment.CommentFromUserId;
            commentDB.CommentToUserId = comment.CommentToUserId;
            _context.SaveChanges();

            return commentDB;
        }

        // перенести в ЮзерРепозитори
        public UserEntity GetUserById(int id)
        {
            try
            {
                return _context.Users.Single(u => u.Id == id && !u.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                //_logger.LogDebug($"{nameof(UserEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $" {(nameof(UserEntity))} with id {id} not found");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }
    }
}
