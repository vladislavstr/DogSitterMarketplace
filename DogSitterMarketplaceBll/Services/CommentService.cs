using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Repositories;

namespace DogSitterMarketplaceBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public List<CommentOrderResponse> GetAllNotDeletedComments()
        {
            var allCommentsEntity = _commentRepository.GetAllComments();
            var commentsEntity = allCommentsEntity.Where(c => !c.IsDeleted && !c.Order.IsDeleted);
            var commentsResponse = _mapper.Map<List<CommentOrderResponse>>(commentsEntity);

            return commentsResponse;
        }

        public CommentOrderResponse GetNotDeletedCommentById(int id)
        {
            var commentEntity = _commentRepository.GetCommentById(id);

            if (!commentEntity.IsDeleted)
            {
                var commentResponse = _mapper.Map<CommentOrderResponse>(commentEntity);

                return commentResponse;
            }
            else
            {
                // что возвращать?  + logger??
                throw new NotFoundException(id, nameof(commentEntity));
            }
        }

        public void DeleteCommentById(int id)
        {
            _commentRepository.DeleteCommentById(id);
        }

        public CommentOrderResponse AddComment(CommentRequest commentRequest)
        {
            var commentEntity = _mapper.Map<CommentEntity>(commentRequest);
            commentRequest.OrderId = _orderRepository.GetOrderById(commentRequest.OrderId).Id;
            commentRequest.CommentFromUserId = _commentRepository.GetUserById(commentRequest.CommentFromUserId).Id;
            commentRequest.CommentToUserId = _commentRepository.GetUserById(commentRequest.CommentToUserId).Id;
            var addCommentEntity = _commentRepository.AddComment(commentEntity);
            var addCommentResponse = _mapper.Map<CommentOrderResponse>(addCommentEntity);

            return addCommentResponse;
        }

        public CommentOrderResponse UpdateComment(CommentUpdate commentUpdate)
        {
            var commentEntity = _mapper.Map<CommentEntity>(commentUpdate);
            commentUpdate.OrderId = _commentRepository.GetOrderById(commentUpdate.OrderId).Id;
            commentUpdate.CommentFromUserId = _commentRepository.GetUserById(commentUpdate.CommentFromUserId).Id;
            commentUpdate.CommentToUserId = _commentRepository.GetUserById(commentUpdate.CommentToUserId).Id;
            var updateCommentEntity = _commentRepository.UpdateComment(commentEntity);
            var commentOrderResponse = _mapper.Map<CommentOrderResponse>(updateCommentEntity);

            return commentOrderResponse;
        }
    }
}
