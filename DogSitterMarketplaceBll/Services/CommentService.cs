using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using NLog;

namespace DogSitterMarketplaceBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly IUserRepository _userRepository;

        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public CommentService(ICommentRepository commentRepository, IOrderRepository orderRepository, IUserRepository userRepository, IOrderService orderService, IMapper mapper, ILogger nLogger)
        {
            _commentRepository = commentRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _orderService = orderService;
            _mapper = mapper;
            _logger = nLogger;
        }
        public async Task<CommentOrderResponse> AddComment(CommentRequest addComment)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(AddComment)}");

            var userCommentFrom = await CheckUserIsExistAndIsNotDeleted(addComment.CommentFromUserId);
            var userCommentTo = await CheckUserIsExistAndIsNotDeleted(addComment.CommentToUserId);
            var orderResponse = await _orderService.CheckAndGetOrderIsExistAndIsNotDeleted(addComment.OrderId);
            var userRoleCommentFrom = await _userRepository.GetUserRoleById(userCommentFrom.RoleId);
            var userRoleCommentTo = await _userRepository.GetUserRoleById(userCommentTo.RoleId);

            if (userRoleCommentFrom.Name == UserRole.Sitter && userRoleCommentTo.Name == UserRole.Client
                && !await CheckOrderBetweenSitterAndClient(userCommentFrom.Id, userCommentTo.Id, orderResponse.Id))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(AddComment)} Order between users with id {userRoleCommentFrom.Id} and id {userRoleCommentTo.Id} not found");
                throw new ArgumentException("Order between users with id {userRoleCommentFrom.Id} and id {userRoleCommentTo.Id} not found");
            }
            else if (userRoleCommentFrom.Name == UserRole.Client && userRoleCommentTo.Name == UserRole.Sitter
                && !await CheckOrderBetweenSitterAndClient(userCommentTo.Id, userCommentFrom.Id, orderResponse.Id))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(AddComment)} Order between users with id {userRoleCommentFrom.Id} and id {userRoleCommentTo.Id} not found");
                throw new ArgumentException("Order between users with id {userRoleCommentFrom.Id} and id {userRoleCommentTo.Id} not found");
            }

            if (userRoleCommentFrom.Name == UserRole.Sitter && userRoleCommentTo.Name == UserRole.Client
                || userRoleCommentFrom.Name == UserRole.Client && userRoleCommentTo.Name == UserRole.Sitter)
            {
                var commentEntity = _mapper.Map<CommentEntity>(addComment);
                var addCommentEntity = await _commentRepository.AddComment(commentEntity);
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

        public async Task<AvgScoreCommentsResponse<T>> GetCommentsAndScoresForUserAboutHim<T>(int userId, string role) where T : CommentResponse
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetCommentsAndScoresForUserAboutHim)}");

            var user = await CheckUserIsExistAndIsNotDeleted(userId);
            var sortDescCommentsEntities = await GetSortedDescComments(userId);
            var userRole = await _userRepository.GetUserRoleById(user.RoleId);

            if (userRole.Name == role)
            {
                var averageScore = GetAverageScoreForSortedDescComments(sortDescCommentsEntities);
                var resultComments = _mapper.Map<List<T>>(sortDescCommentsEntities);
                var resultAvgComments = new AvgScoreCommentsResponse<T>
                {
                    AverageScore = averageScore,
                    Comments = resultComments
                };

                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetCommentsAndScoresForUserAboutHim)}");

                return resultAvgComments;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetCommentsAndScoresForUserAboutHim)} {nameof(CommentEntity)} User has not got necessity UserRole for getComments");
                throw new ArgumentException("User has not got necessity UserRole for getComments");
            }
        }

        public async Task<AvgScoreCommentsResponse<T>> GetCommentsAndScoresAboutOtherUsers<T>(int userIdGetComment, string roleUserGetComment,
                                                                                                int userIdToComment, string roleUserToComment) where T : CommentResponse
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetCommentsAndScoresAboutOtherUsers)}");

            var commentUserToResponse = await CheckUserIsExistAndIsNotDeleted(userIdToComment);
            var userWhoGetCommentResponse = await CheckUserIsExistAndIsNotDeleted(userIdGetComment);
            var sortDescCommentsEntities = await GetSortedDescComments(userIdToComment);
            var userRoleWhoGetComment = await _userRepository.GetUserRoleById(userWhoGetCommentResponse.RoleId);
            var userRoleCommentTo = await _userRepository.GetUserRoleById(commentUserToResponse.RoleId);

            if (userRoleWhoGetComment.Name == roleUserGetComment && userRoleCommentTo.Name == roleUserToComment)
            {
                var averageScore = GetAverageScoreForSortedDescComments(sortDescCommentsEntities);
                var resultComments = _mapper.Map<List<T>>(sortDescCommentsEntities);
                var resultAvgComments = new AvgScoreCommentsResponse<T>
                {
                    AverageScore = averageScore,
                    Comments = resultComments
                };

                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetCommentsAndScoresAboutOtherUsers)}");

                return resultAvgComments;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetCommentsAndScoresAboutOtherUsers)} {nameof(CommentEntity)} One or more of users has not got necessity UserRole for getComments");
                throw new ArgumentException("One or more of users has not got necessity UserRole for getComments");
            }
        }

        public async Task<List<CommentOrderResponse>> GetAllNotDeletedComments()
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetAllNotDeletedComments)}");

            var allCommentsEntity = await _commentRepository.GetAllComments();
            var commentsEntity = allCommentsEntity.Where(c => !c.IsDeleted && !c.Order.IsDeleted);
            var commentsResponse = _mapper.Map<List<CommentOrderResponse>>(commentsEntity);

            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetAllNotDeletedComments)}");

            return commentsResponse;
        }

        public async Task<CommentOrderResponse> GetNotDeletedCommentById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(GetNotDeletedCommentById)}");

            var commentEntity = await _commentRepository.GetCommentById(id);

            if (!commentEntity.IsDeleted)
            {
                var commentResponse = _mapper.Map<CommentOrderResponse>(commentEntity);
                _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(GetNotDeletedCommentById)}");

                return commentResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(GetNotDeletedCommentById)} {nameof(CommentEntity)} with id {id} is deleted.");
                throw new NotFoundException(id, nameof(commentEntity));
            }
        }

        public async Task DeleteCommentById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(DeleteCommentById)}");

            await _commentRepository.DeleteCommentById(id);

            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(DeleteCommentById)}");
        }

        public async Task<CommentOrderResponse> UpdateComment(CommentUpdate commentUpdate)
        {
            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} start {nameof(UpdateComment)}");

            var commentEntity = _mapper.Map<CommentEntity>(commentUpdate);
            commentUpdate.OrderId = (await _orderRepository.GetOrderById(commentUpdate.OrderId)).Id;
            commentUpdate.CommentFromUserId = (await _userRepository.GetUserWithRoleById(commentUpdate.CommentFromUserId)).Id;
            commentUpdate.CommentToUserId = (await _userRepository.GetUserWithRoleById(commentUpdate.CommentToUserId)).Id;
            var updateCommentEntity = await _commentRepository.UpdateComment(commentEntity);
            var commentOrderResponse = _mapper.Map<CommentOrderResponse>(updateCommentEntity);

            _logger.Log(LogLevel.Info, $"{nameof(CommentService)} end {nameof(UpdateComment)}");

            return commentOrderResponse;
        }

        private async Task<UserShortResponse> CheckUserIsExistAndIsNotDeleted(int userId)
        {
            var userEntity = await _userRepository.GetExistAndNotDeletedUserById(userId);
            var userResponse = _mapper.Map<UserShortResponse>(userEntity);

            return userResponse;
        }

        private async Task<List<CommentEntity>> GetSortedDescComments(int userIdToComment)
        {
            var commentsEntities = await _commentRepository.GetAllCommentsAndScoresByUserId(userIdToComment);
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

        private async Task<bool> CheckOrderBetweenSitterAndClient(int sitterId, int clientId, int orderId)
        {
            var allOrders = await _orderRepository.GetOrdersBySitterIdAndClientId(sitterId, clientId);

            return allOrders.Any(o => o.Id == orderId);
        }
    }
}
