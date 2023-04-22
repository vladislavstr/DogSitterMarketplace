using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Repositories;
using NLog;
using System.Xml.Linq;

namespace DogSitterMarketplaceBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public CommentService(ICommentRepository commentRepository, IOrderRepository orderRepository, IOrderService orderService, IMapper mapper, ILogger nLogger)
        {
            _commentRepository = commentRepository;
            _orderRepository = orderRepository;
            _orderService = orderService;
            _mapper = mapper;
            _logger = nLogger;
        }
        public CommentOrderResponse AddComment(CommentRequest addComment)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(AddComment)}");
            var userCommentFrom = CheckUserIsExistAndIsNotDeleted(addComment.CommentFromUserId);
            var userCommentTo = CheckUserIsExistAndIsNotDeleted(addComment.CommentToUserId);
            var orderResponse = _orderService.CheckOrderIsExistAndIsNotDeleted(addComment.OrderId);

            if (userCommentFrom.RoleId == 3 && userCommentTo.RoleId == 4
                || userCommentFrom.RoleId == 4 && userCommentTo.RoleId == 3)
            {
                var commentEntity = _mapper.Map<CommentEntity>(addComment);
                var addCommentEntity = _commentRepository.AddComment(commentEntity);
                var addCommentResponse = _mapper.Map<CommentOrderResponse>(addCommentEntity);
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(AddComment)}");

                return addCommentResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(AddComment)} One or more users can not give/take comment");
                throw new ArgumentException("One or more users can not give/take comment");
            }
        }

        public AvgScoreCommentsAboutSitterForClientResponse GetCommentsAndScoresForClientAboutSitter(int userIdGetComment, int userIdToComment)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetCommentsAndScoresForClientAboutSitter)}");
            var commentUserToResponse = CheckUserIsExistAndIsNotDeleted(userIdToComment);
            var userWhoGetCommentResponse = CheckUserIsExistAndIsNotDeleted(userIdGetComment);
            var sortDescCommentsEntities = SortDescComments(userIdToComment);

            if (userWhoGetCommentResponse.RoleId == 3 && commentUserToResponse.RoleId == 4)
            {
                var averageScore = GetAverageScoreForSortedDescComments(sortDescCommentsEntities);
                var resultComments = _mapper.Map<List<CommentAboutSitterForClientResponse>>(sortDescCommentsEntities);
                var resultAvgComments = new AvgScoreCommentsAboutSitterForClientResponse
                {
                    AverageScore = averageScore,
                    CommentsAboutSitterForClient = resultComments
                };
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetCommentsAndScoresForClientAboutSitter)}");
                return resultAvgComments;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetCommentsAndScoresForClientAboutSitter)} {nameof(CommentEntity)} One or more of users has not got necessity UserRole for getComments");
                throw new ArgumentException("One or more of users has not got necessity UserRole for getComments");
            }
        }

        public AvgScoreCommentAboutClientForSitterResponse GetCommentsAndScoresForSitterAboutClient(int userIdGetComment, int userIdToComment)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetCommentsAndScoresForSitterAboutClient)}");
            var commentUserToResponse = CheckUserIsExistAndIsNotDeleted(userIdToComment);
            var userWhoGetCommentResponse = CheckUserIsExistAndIsNotDeleted(userIdGetComment);
            var sortDescCommentsEntities = SortDescComments(userIdToComment);

            if (userWhoGetCommentResponse.RoleId == 4 && commentUserToResponse.RoleId == 3)
            {
                var averageScore = GetAverageScoreForSortedDescComments(sortDescCommentsEntities);
                var resultComments = _mapper.Map<List<CommentAboutClientsForSitterResponse>>(sortDescCommentsEntities);
                var resultAvgComments = new AvgScoreCommentAboutClientForSitterResponse
                {
                    AverageScore = averageScore,
                    CommentsAboutClientForSitter = resultComments
                };
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetCommentsAndScoresForSitterAboutClient)}");
                return resultAvgComments;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetCommentsAndScoresForSitterAboutClient)} {nameof(CommentEntity)} One or more of users has not got necessity UserRole for getComments");
                throw new ArgumentException("One or more of users has not got necessity UserRole for getComments");
            }
        }

        public AvgScoreCommentResponse GetCommentsAndScoresForClientAboutHim(int userId)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetCommentsAndScoresForClientAboutHim)}");
            var user = CheckUserIsExistAndIsNotDeleted(userId);
            var sortDescCommentsEntities = SortDescComments(userId);

            if (user.RoleId == 3)
            {
                var averageScore = GetAverageScoreForSortedDescComments(sortDescCommentsEntities);
                var resultComments = _mapper.Map<List<CommentResponse>>(sortDescCommentsEntities);
                var resultAvgComments = new AvgScoreCommentResponse
                {
                    AverageScore = averageScore,
                    Comments = resultComments
                };
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetCommentsAndScoresForClientAboutHim)}");
                return resultAvgComments;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetCommentsAndScoresForClientAboutHim)} {nameof(CommentEntity)} User has not got necessity UserRole for getComments");
                throw new ArgumentException("User has not got necessity UserRole for getComments");
            }
        }

        public AvgScoreCommentWithoutUserResponse GetCommentsAndScoresForSitterAboutHim(int userId)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetCommentsAndScoresForSitterAboutHim)}");
            var user = CheckUserIsExistAndIsNotDeleted(userId);
            var sortDescCommentsEntities = SortDescComments(userId);

            if (user.RoleId == 4)
            {
                var averageScore = GetAverageScoreForSortedDescComments(sortDescCommentsEntities);
                var resultComments = _mapper.Map<List<CommentWithoutUserResponse>>(sortDescCommentsEntities);
                var resultAvgComments = new AvgScoreCommentWithoutUserResponse
                {
                    AverageScore = averageScore,
                    CommentsWithoutUser = resultComments
                };
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetCommentsAndScoresForSitterAboutHim)}");
                return resultAvgComments;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetCommentsAndScoresForSitterAboutHim)} {nameof(CommentEntity)} User has not got necessity UserRole for getComments");
                throw new ArgumentException("User has not got necessity UserRole for getComments");
            }
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

        //проверку перенести в Юзерсервис
        private UserShortResponse CheckUserIsExistAndIsNotDeleted(int userId)
        {
            //метод перенести в ЮзерРепо
            var userEntity = _orderRepository.GetExistAndNotDeletedUserById(userId);
            var userResponse = _mapper.Map<UserShortResponse>(userEntity);

            return userResponse;
        }

        private List<CommentEntity> SortDescComments(int userIdToComment)
        {
            var commentsEntities = _commentRepository.GetAllCommentsAndScoresByUserId(userIdToComment);
            var sortDescCommentsEntities = commentsEntities.OrderByDescending(c => c.Order.DateStart).ToList();

            return sortDescCommentsEntities;
        }

        private decimal GetAverageScoreForSortedDescComments(List<CommentEntity> sortedDescComments)
        {
            var firstThirtyComments = new List<CommentEntity>();

            if (sortedDescComments.Count >= 1 && sortedDescComments.Count < 30)
            {
                firstThirtyComments = sortedDescComments.GetRange(0, sortedDescComments.Count);
            }
            else if (sortedDescComments.Count >= 30)
            {
                firstThirtyComments = sortedDescComments.GetRange(0, 30);
            }
            else
            {
                return 0;
            }

            var average = (decimal)firstThirtyComments.Average(c => c.Score);

            return average;
        }
    }
}
