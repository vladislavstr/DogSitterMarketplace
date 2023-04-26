﻿using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceBll.Tests.TestCaseSource;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using FluentAssertions;
using Moq;
using NLog;

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
