using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Repositories;
using NLog;

namespace DogSitterMarketplaceBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public CommentService(ICommentRepository commentRepository, IOrderRepository orderRepository, IMapper mapper, ILogger nLogger)
        {
            _commentRepository = commentRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = nLogger;
        }

        public List<CommentOrderResponse> GetAllNotDeletedComments()
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetAllNotDeletedComments)}");
            var allCommentsEntity = _commentRepository.GetAllComments();
            var commentsEntity = allCommentsEntity.Where(c => !c.IsDeleted && !c.Order.IsDeleted);
            var commentsResponse = _mapper.Map<List<CommentOrderResponse>>(commentsEntity);
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetAllNotDeletedComments)}");

            return commentsResponse;
        }

        public CommentOrderResponse GetNotDeletedCommentById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetNotDeletedCommentById)}");
            var commentEntity = _commentRepository.GetCommentById(id);

            if (!commentEntity.IsDeleted)
            {
                var commentResponse = _mapper.Map<CommentOrderResponse>(commentEntity);
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetNotDeletedCommentById)}");

                return commentResponse;
            }
            else
            {
                //_logger.LogDebug($"{nameof(CommentService)} {nameof(GetNotDeletedCommentById)} {nameof(CommentEntity)} with id {id} is deleted.");
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetNotDeletedCommentById)} {nameof(CommentEntity)} with id {id} is deleted.");
                throw new NotFoundException(id, nameof(commentEntity));
            }
        }

        public void DeleteCommentById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(DeleteCommentById)}");
            _commentRepository.DeleteCommentById(id);
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(DeleteCommentById)}");
        }

        public CommentOrderResponse AddComment(CommentRequest commentRequest)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(AddComment)}");
            var commentEntity = _mapper.Map<CommentEntity>(commentRequest);
            commentRequest.OrderId = _orderRepository.GetOrderById(commentRequest.OrderId).Id;
            commentRequest.CommentFromUserId = _commentRepository.GetUserById(commentRequest.CommentFromUserId).Id;
            commentRequest.CommentToUserId = _commentRepository.GetUserById(commentRequest.CommentToUserId).Id;
            var addCommentEntity = _commentRepository.AddComment(commentEntity);
            var addCommentResponse = _mapper.Map<CommentOrderResponse>(addCommentEntity);
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(AddComment)}");

            return addCommentResponse;
        }

        public CommentOrderResponse UpdateComment(CommentUpdate commentUpdate)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(UpdateComment)}");
            var commentEntity = _mapper.Map<CommentEntity>(commentUpdate);
            commentUpdate.OrderId = _orderRepository.GetOrderById(commentUpdate.OrderId).Id;
            commentUpdate.CommentFromUserId = _commentRepository.GetUserById(commentUpdate.CommentFromUserId).Id;
            commentUpdate.CommentToUserId = _commentRepository.GetUserById(commentUpdate.CommentToUserId).Id;
            var updateCommentEntity = _commentRepository.UpdateComment(commentEntity);
            var commentOrderResponse = _mapper.Map<CommentOrderResponse>(updateCommentEntity);
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(UpdateComment)}");

            return commentOrderResponse;
        }
    }
}
