using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public List<CommentOrderResponse> GetAllComments()
        {
            var commentsEntity = _commentRepository.GetAllComments();
            var commentsResponse = _mapper.Map<List<CommentOrderResponse>>(commentsEntity);

            return commentsResponse;
        }

        public CommentOrderResponse GetCommentById(int id)
        {
            var commentEntity = _commentRepository.GetCommentById(id);
            var commentResponse = _mapper.Map<CommentOrderResponse>(commentEntity);

            return commentResponse; 
        }

        public void DeleteCommentById(int id)
        {
            _commentRepository.DeleteCommentById(id);
        }

        public CommentOrderResponse AddComment(CommentRequest commentRequest)
        {
            var commentEntity = _mapper.Map<CommentEntity>(commentRequest);
            var addCommentEntity = _commentRepository.AddComment(commentEntity);
            var addCommentResponse = _mapper.Map<CommentOrderResponse>(addCommentEntity);

            return addCommentResponse;
        }

        public int UpdateComment(CommentUpdate commentUpdate)
        {
            var commentEntity = _mapper.Map<CommentEntity>(commentUpdate);
            var orderEtity = _commentRepository.GetOrderById(commentUpdate.OrderId);
            var userCommentFromEntity = _commentRepository.GetUserById(commentUpdate.CommentFromUserId);
            var userCommentToEntity = _commentRepository.GetUserById(commentUpdate.CommentToUserId);
            var id = _commentRepository.UpdateComment(commentEntity);

            return id;
        }
    }
}
