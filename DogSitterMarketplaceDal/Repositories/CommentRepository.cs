using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DogSitterMarketplaceDal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger<ICommentRepository> _logger;

        public CommentRepository(OrdersAndPetsAndCommentsContext context, ILogger<ICommentRepository> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogDebug($"{ex}, {nameof(CommentRepository)} {nameof(CommentEntity)} {nameof(AddComment)}");
                throw new ArgumentException();
            }
        }

        public CommentEntity UpdateComment(CommentEntity comment)
        {
            var commentDB = _context.Comments.SingleOrDefault(c => c.Id == comment.Id && !c.IsDeleted);

            if (commentDB == null)
            {
                _logger.LogDebug($"{nameof(CommentRepository)} {nameof(UpdateComment)} {(nameof(CommentEntity))} with id {comment.Id} not found");
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


        // Этот метод есть в Ордер Репозитории
        public OrderEntity GetOrderById(int id)
        {
            try
            {
                return _context.Orders.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                _logger.LogDebug($"{nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public UserEntity GetUserById(int id)
        {
            try
            {
                return _context.Users.Single(u => u.Id == id && !u.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                _logger.LogDebug($"{nameof(UserEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }
    }
}
