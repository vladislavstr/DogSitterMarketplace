using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceBll.Tests.TestCaseSource;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using FluentAssertions;
using Moq;
using NLog;
using NUnit.Framework.Constraints;

namespace DogSitterMarketplaceBll.Tests
{
    public class CommentServiceTests
    {
        private CommentService _commentService;

        private Mock<ICommentRepository> _mockCommentRepo;

        private Mock<IOrderRepository> _mockOrderRepo;

        private Mock<IUserRepository> _mockUserRepo;

        private Mock<IOrderService> _mockOrderService;

        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperBllCommentProfile>();
                cfg.AddProfile<MapperBllPetProfile>();
                cfg.AddProfile<MapperBllOrderProfile>();
            }).CreateMapper();
            var logger = LogManager.Setup().GetCurrentClassLogger();
            _mockCommentRepo = new Mock<ICommentRepository>();
            _mockOrderRepo = new Mock<IOrderRepository>();
            _mockUserRepo = new Mock<IUserRepository>();
            _mockOrderService = new Mock<IOrderService>();
            _commentService = new CommentService(
                                            _mockCommentRepo.Object,
                                            _mockOrderRepo.Object,
                                            _mockUserRepo.Object,
                                            _mockOrderService.Object,
                                            _mapper,
                                            logger);
        }

        [TestCaseSource(typeof(CommentServiceTestCaseSource), nameof(CommentServiceTestCaseSource.AddComment_WhenCommentFromUserToSitterTestCaseSource))]
        public void AddCommentTest_WhenCommentFromUserToSitter(int userCommentFromId, UserEntity userCommentFromEntity, int userCommentToId, UserEntity userCommentToEntity,
                                    int userRoleCommentFromId, UserRoleEntity userRoleCommentFromEntity, int userRoleCommentToId, UserRoleEntity userRoleCommentToEntity,
                                    List<OrderEntity> allOrders, CommentEntity commentEntity, CommentEntity addCommentEntity, CommentRequest addComment,
                                    CommentOrderResponse expected, int orderId, OrderResponse orderResponse)
        {
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentFromId)).Returns(userCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentToId)).Returns(userCommentToEntity);
            _mockOrderService.Setup(os => os.CheckOrderIsExistAndIsNotDeleted(orderId)).Returns(orderResponse);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleCommentFromId)).Returns(userRoleCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleCommentToId)).Returns(userRoleCommentToEntity);
            // _mockOrderRepo.Setup(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId)).Returns(allOrders);
            _mockOrderRepo.Setup(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId)).Returns(allOrders);
            _mockCommentRepo.Setup(c => c.AddComment(It.Is<CommentEntity>(c => method(c, commentEntity)))).Returns(addCommentEntity);

            CommentOrderResponse actual = _commentService.AddComment(addComment);

            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentToId), Times.Once);
            _mockOrderService.Verify(os => os.CheckOrderIsExistAndIsNotDeleted(orderId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentToId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId), Times.Once);
            _mockCommentRepo.Verify(c => c.AddComment(It.IsAny<CommentEntity>()), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(CommentServiceTestCaseSource), nameof(CommentServiceTestCaseSource.AddComment_WhenCommentFromSitterToUserTestCaseSource))]
        public void AddCommentTest_WhenCommentFromSitterToUser(int userCommentFromId, UserEntity userCommentFromEntity, int userCommentToId, UserEntity userCommentToEntity,
                                    int userRoleCommentFromId, UserRoleEntity userRoleCommentFromEntity, int userRoleCommentToId, UserRoleEntity userRoleCommentToEntity,
                                    List<OrderEntity> allOrders, CommentEntity commentEntity, CommentEntity addCommentEntity, CommentRequest addComment,
                                    CommentOrderResponse expected, int orderId, OrderResponse orderResponse)
        {
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentFromId)).Returns(userCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentToId)).Returns(userCommentToEntity);
            _mockOrderService.Setup(os => os.CheckOrderIsExistAndIsNotDeleted(orderId)).Returns(orderResponse);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleCommentFromId)).Returns(userRoleCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleCommentToId)).Returns(userRoleCommentToEntity);
            _mockOrderRepo.Setup(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId)).Returns(allOrders);
            //_mockOrderRepo.Setup(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId)).Returns(allOrders);
            _mockCommentRepo.Setup(c => c.AddComment(It.Is<CommentEntity>(c => method(c, commentEntity)))).Returns(addCommentEntity);

            CommentOrderResponse actual = _commentService.AddComment(addComment);

            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentToId), Times.Once);
            _mockOrderService.Verify(os => os.CheckOrderIsExistAndIsNotDeleted(orderId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentToId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId), Times.Never);
            _mockCommentRepo.Verify(c => c.AddComment(It.IsAny<CommentEntity>()), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(CommentServiceTestCaseSource), nameof(CommentServiceTestCaseSource.AddComment__WhenOrderBetweenSitterAndClientIsNotExist_CommentFromClientToUser_ShouldBeArgumentException_TestCaseSource))]
        public void AddCommentTest_WhenOrderBetweenSitterAndClientIsNotExist_CommentFromClientToUser_ShouldBeArgumentException(int userCommentFromId, UserEntity userCommentFromEntity,
                                    int userCommentToId, UserEntity userCommentToEntity,int userRoleCommentFromId, UserRoleEntity userRoleCommentFromEntity, int userRoleCommentToId, 
                                    UserRoleEntity userRoleCommentToEntity, CommentRequest addComment, int orderId, OrderResponse orderResponse)
        {
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentFromId)).Returns(userCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentToId)).Returns(userCommentToEntity);
            _mockOrderService.Setup(os => os.CheckOrderIsExistAndIsNotDeleted(orderId)).Returns(orderResponse);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleCommentFromId)).Returns(userRoleCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleCommentToId)).Returns(userRoleCommentToEntity);
            //_mockOrderRepo.Setup(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId)).Returns(allOrders);
            _mockOrderRepo.Setup(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId)).Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() => _commentService.AddComment(addComment));

            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentToId), Times.Once);
            _mockOrderService.Verify(os => os.CheckOrderIsExistAndIsNotDeleted(orderId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentToId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId), Times.Once);
            _mockCommentRepo.Verify(c => c.AddComment(It.IsAny<CommentEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(CommentServiceTestCaseSource), nameof(CommentServiceTestCaseSource.AddComment_WhenOrderIsNotExist_ShouldBeNotFoundException_TestCaseSource))]
        public void AddCommentTest_WhenOrderIsNotExist_ShouldBeNotFoundException(int userCommentFromId, UserEntity userCommentFromEntity,
                                                                                int userCommentToId, UserEntity userCommentToEntity, int userRoleCommentFromId,
                                                                                int userRoleCommentToId, CommentRequest addComment, int orderId)
        {
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentFromId)).Returns(userCommentFromEntity);
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userCommentToId)).Returns(userCommentToEntity);
            _mockOrderService.Setup(os => os.CheckOrderIsExistAndIsNotDeleted(orderId)).Throws(() => new NotFoundException(orderId, "OrderEntity"));

            Assert.Throws<NotFoundException>(() => _commentService.AddComment(addComment));

            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentFromId), Times.Once);
            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userCommentToId), Times.Once);
            _mockOrderService.Verify(os => os.CheckOrderIsExistAndIsNotDeleted(orderId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentFromId), Times.Never);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleCommentToId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentFromId, userCommentToId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersBySitterIdAndClientId(userCommentToId, userCommentFromId), Times.Never);
            _mockCommentRepo.Verify(c => c.AddComment(It.IsAny<CommentEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(CommentServiceTestCaseSource), nameof(CommentServiceTestCaseSource.GetCommentsAndScoresForUserAboutHim_ForClientAboutHimTestCaseSource))]
        public void GetCommentsAndScoresForUserAboutHim_ForClientAboutHimTest(int userId, UserEntity userEntity, List<CommentEntity> commentsEntities, int userRoleId, 
                                                                              UserRoleEntity userRole, AvgScoreCommentsResponse<CommentWithUserShortResponse> expected)
        {
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userId)).Returns(userEntity);
            _mockCommentRepo.Setup(c => c.GetAllCommentsAndScoresByUserId(userId)).Returns(commentsEntities);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleId)).Returns(userRole);

            AvgScoreCommentsResponse<CommentWithUserShortResponse> actual = _commentService.GetCommentsAndScoresForUserAboutHim<CommentWithUserShortResponse>(userId, UserRole.Client);

            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userId), Times.Once);
            _mockCommentRepo.Verify(c => c.GetAllCommentsAndScoresByUserId(userId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleId), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(CommentServiceTestCaseSource), nameof(CommentServiceTestCaseSource.GetCommentsAndScoresForUserAboutHim_ForSitterAboutHimTestCaseSource))]
        public void GetCommentsAndScoresForUserAboutHim_ForSitterAboutHimTest(int userId, UserEntity userEntity, List<CommentEntity> commentsEntities, int userRoleId,
                                                                              UserRoleEntity userRole, AvgScoreCommentsResponse<CommentResponse> expected)
        {
            _mockUserRepo.Setup(u => u.GetExistAndNotDeletedUserById(userId)).Returns(userEntity);
            _mockCommentRepo.Setup(c => c.GetAllCommentsAndScoresByUserId(userId)).Returns(commentsEntities);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleId)).Returns(userRole);

            AvgScoreCommentsResponse<CommentResponse> actual = _commentService.GetCommentsAndScoresForUserAboutHim<CommentResponse>(userId, UserRole.Sitter);

            _mockUserRepo.Verify(u => u.GetExistAndNotDeletedUserById(userId), Times.Once);
            _mockCommentRepo.Verify(c => c.GetAllCommentsAndScoresByUserId(userId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleId), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        private bool method(CommentEntity c, CommentEntity commentEntity)
        {
            try
            {
                c.Should().BeEquivalentTo(commentEntity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
