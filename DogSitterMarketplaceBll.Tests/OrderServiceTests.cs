using AutoMapper;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceBll.Tests.TestCaseSource;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using FluentAssertions;
using Moq;
using NLog;

namespace DogSitterMarketplaceBll.Tests
{
    public class OrderServiceTests
    {
        private OrderService _orderService;

        private Mock<IOrderRepository> _mockOrderRepo;

        private Mock<IPetRepository> _mockPetRepo;

        private Mock<IUserRepository> _mockUserRepo;

        private Mock<IWorkAndLocationRepository> _mockWorkLocationRepo;

        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperBllOrderProfile>();
                cfg.AddProfile<MapperBllPetProfile>();
            }).CreateMapper();
            var logger = LogManager.Setup().GetCurrentClassLogger();
            _mockOrderRepo = new Mock<IOrderRepository>();
            _mockPetRepo = new Mock<IPetRepository>();
            _mockUserRepo = new Mock<IUserRepository>();
            _mockWorkLocationRepo = new Mock<IWorkAndLocationRepository>();
            _orderService = new OrderService(
                                            _mockOrderRepo.Object,
                                            _mockPetRepo.Object,
                                            _mockUserRepo.Object,
                                            _mockWorkLocationRepo.Object,
                                            _mapper,
                                            logger);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.DeleteOrderByIdTestCaseSource))]
        public async Task DeleteOrderByIdTest(int id)
        {
            //_mockOrderRepo.Setup(o => o.DeleteOrderById(id));

            await _orderService.DeleteOrderById(id);

            _mockOrderRepo.Verify(o => o.DeleteOrderById(id), Times.Once);
            _mockOrderRepo.VerifyNoOtherCalls();
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.DeleteOrderByIdTest_WhenIdIsNotExist_ShouldNotFoundExceptionTestCaseSource))]
        public void DeleteOrderByIdTest_WhenIdIsNotExist_ShouldNotFoundException(int id)
        {
            _mockOrderRepo.Setup(o => o.DeleteOrderById(id)).ThrowsAsync(new NotFoundException(id, nameof(OrderEntity)));

            Assert.ThrowsAsync<NotFoundException>(async () => await _orderService.DeleteOrderById(id));

            _mockOrderRepo.Verify(o => o.DeleteOrderById(id), Times.Once);
            _mockOrderRepo.VerifyNoOtherCalls();
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrderTestCaseSource))]
        public async Task AddOrderTest(List<int> petsId, List<PetEntity> allPets, List<string> messagesOfIsDeleted, OrderEntity orderEntity,
                                OrderEntity addOrderEntity, OrderCreateRequest newOrder, OrderResponse expected, int sitterId, SitterWorkEntity sitterWork,
                                List<SitterWorkEntity> allSitterWorks, DateTime startDateOrder, List<OrderEntity> allOrdersBySitter, int sitterWorkId,
                                OrderStatusEntity orderStatusUnderConsideration)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).ReturnsAsync(allPets);
            _mockWorkLocationRepo.Setup(o => o.GetNotDeletedSitterWorkById(sitterWorkId)).ReturnsAsync(sitterWork);
            _mockWorkLocationRepo.Setup(o => o.GetAllSitterWorksByUserId(sitterId)).ReturnsAsync(allSitterWorks);
            _mockOrderRepo.Setup(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder)).ReturnsAsync(allOrdersBySitter);
            _mockOrderRepo.Setup(o => o.GetOrderStatusByName("under consideration")).ReturnsAsync(orderStatusUnderConsideration);
            _mockOrderRepo.Setup(o => o.AddNewOrder(It.Is<OrderEntity>(o => Compare(o, orderEntity)))).ReturnsAsync(addOrderEntity);

            OrderResponse actual = await _orderService.AddOrder(newOrder);

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetNotDeletedSitterWorkById(sitterWorkId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusByName("under consideration"), Times.Once);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenPetIsNotExist_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenPetIsNotExist_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder,
                                                                            int sitterId, DateTime startDateOrder, int sitterWorkId)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).ReturnsAsync(allPets);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetNotDeletedSitterWorkById(sitterWorkId), Times.Never);
            _mockWorkLocationRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenPetsBelongToDifferentUsers_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenPetsBelongToDifferentUsers_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder,
                                                                            int sitterId, DateTime startDateOrder, int sitterWorkId)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).ReturnsAsync(allPets);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetNotDeletedSitterWorkById(sitterWorkId), Times.Never);
            _mockWorkLocationRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenDateStartOrderNotEarlierThenDateEndOrder_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenDateStartOrderNotEarlierThenDateEndOrder_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder,
                                                                            int sitterId, DateTime startDateOrder, int sitterWorkId)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).ReturnsAsync(allPets);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetNotDeletedSitterWorkById(sitterWorkId), Times.Never);
            _mockWorkLocationRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenSitterHasNotTimingToOrder_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenSitterHasNotTimingToOrder_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder, int sitterId,
                                                                            DateTime startDateOrder, int sitterWorkId, SitterWorkEntity sitterWork, List<SitterWorkEntity> allSitterWorks)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).ReturnsAsync(allPets);
            _mockWorkLocationRepo.Setup(o => o.GetNotDeletedSitterWorkById(sitterWorkId)).ReturnsAsync(sitterWork);
            _mockWorkLocationRepo.Setup(o => o.GetAllSitterWorksByUserId(sitterId)).ReturnsAsync(allSitterWorks);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetNotDeletedSitterWorkById(sitterWorkId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenSitterIsNotFreeToNewOrder_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenSitterIsNotFreeToNewOrder_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder, int sitterId,
                                                                            DateTime startDateOrder, int sitterWorkId, SitterWorkEntity sitterWork, List<SitterWorkEntity> allSitterWorks,
                                                                            List<OrderEntity> allOrdersBySitter)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).ReturnsAsync(allPets);
            _mockWorkLocationRepo.Setup(o => o.GetNotDeletedSitterWorkById(sitterWorkId)).ReturnsAsync(sitterWork);
            _mockWorkLocationRepo.Setup(o => o.GetAllSitterWorksByUserId(sitterId)).ReturnsAsync(allSitterWorks);
            _mockOrderRepo.Setup(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder)).ReturnsAsync(allOrdersBySitter);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetNotDeletedSitterWorkById(sitterWorkId), Times.Once);
            _mockWorkLocationRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Once);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.ChangeOrderStatusTestCaseSource))]
        public async Task ChangeOrderStatusTest(int orderId, OrderEntity orderEntity, int orderStatusId, OrderStatusEntity orderStatus,
                                          OrderEntity updateOrderEntity, OrderResponse expected)
        {
            _mockOrderRepo.Setup(o => o.GetOrderById(orderId)).ReturnsAsync(orderEntity);
            _mockOrderRepo.Setup(o => o.GetOrderStatusById(orderStatusId)).ReturnsAsync(orderStatus);
            _mockOrderRepo.Setup(o => o.ChangeOrderStatus(orderId, orderStatusId)).ReturnsAsync(updateOrderEntity);

            OrderResponse actual = await _orderService.ChangeOrderStatus(orderId, orderStatusId);

            _mockOrderRepo.Verify(o => o.GetOrderById(orderId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusById(orderStatusId), Times.Once);
            _mockOrderRepo.Verify(o => o.ChangeOrderStatus(orderId, orderStatusId), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.ChangeOrderStatus_WhenNewOrderStatusIsNotExist_ShouldBeArgumentException_TestCaseSource))]
        public void ChangeOrderStatusTest_WhenNewOrderStatusIsNotExist_ShouldBeArgumentException(int orderId, OrderEntity orderEntity, int orderStatusId, OrderStatusEntity orderStatus)
        {
            _mockOrderRepo.Setup(o => o.GetOrderById(orderId)).ReturnsAsync(orderEntity);
            _mockOrderRepo.Setup(o => o.GetOrderStatusById(orderStatusId)).ReturnsAsync(orderStatus);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.ChangeOrderStatus(orderId, orderStatusId)); ;

            _mockOrderRepo.Verify(o => o.GetOrderById(orderId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusById(orderStatusId), Times.Once);
            _mockOrderRepo.Verify(o => o.ChangeOrderStatus(orderId, orderStatusId), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.ChangeOrderStatus_WhenCanNotChangeToAtWork_ShouldBeArgumentException_TestCaseSource))]
        public void ChangeOrderStatusTest_WhenCanNotChangeToAtWork_ShouldBeArgumentException(int orderId, OrderEntity orderEntity, int orderStatusId, OrderStatusEntity orderStatus)
        {
            _mockOrderRepo.Setup(o => o.GetOrderById(orderId)).ReturnsAsync(orderEntity);
            _mockOrderRepo.Setup(o => o.GetOrderStatusById(orderStatusId)).ReturnsAsync(orderStatus);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.ChangeOrderStatus(orderId, orderStatusId)); ;

            _mockOrderRepo.Verify(o => o.GetOrderById(orderId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusById(orderStatusId), Times.Once);
            _mockOrderRepo.Verify(o => o.ChangeOrderStatus(orderId, orderStatusId), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.GetAllOrdersUnderConsiderationBySitterIdTestCaseSource))]
        public async Task GetAllOrdersUnderConsiderationBySitterIdTest(int userId, UserEntity userEntity, List<OrderEntity> allOrdersEntities, int userRoleId, UserRoleEntity userRoleEntity,
                                                                 List<OrderResponse> expected)
        {
            _mockUserRepo.Setup(u => u.GetUserWithRoleById(userId)).ReturnsAsync(userEntity);
            _mockOrderRepo.Setup(o => o.GetAllOrdersBySitterId(userId)).ReturnsAsync(allOrdersEntities);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleId)).ReturnsAsync(userRoleEntity);

            List<OrderResponse> actual = await _orderService.GetAllOrdersUnderConsiderationBySitterId(userId);

            _mockUserRepo.Verify(u => u.GetUserWithRoleById(userId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetAllOrdersBySitterId(userId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleId), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.GetAllOrdersUnderConsiderationBySitterId_WhenUserIsNotExist_ShouldBeNotFoundException_TestCaseSource))]
        public void GetAllOrdersUnderConsiderationBySitterIdTest_WhenUserIsNotExist_ShouldBeNotFoundException(int userId, int userRoleId)
        {
            _mockUserRepo.Setup(u => u.GetUserWithRoleById(userId)).ThrowsAsync(new NotFoundException(userId, "UserEntity"));

            Assert.ThrowsAsync<NotFoundException>(async () => await _orderService.GetAllOrdersUnderConsiderationBySitterId(userId));

            _mockUserRepo.Verify(u => u.GetUserWithRoleById(userId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetAllOrdersBySitterId(userId), Times.Never);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleId), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.GetAllOrdersUnderConsiderationBySitterId_WhenUserRoleIsNotSitter_ShouldBeArgumentException_TestCaseSource))]
        public void GetAllOrdersUnderConsiderationBySitterIdTest_WhenUserRoleIsNotSitter_ShouldBeArgumentException(int userId, UserEntity userEntity,
                                                                                                              List<OrderEntity> allOrdersEntities, int userRoleId, UserRoleEntity userRoleEntity)
        {
            _mockUserRepo.Setup(u => u.GetUserWithRoleById(userId)).ReturnsAsync(userEntity);
            _mockOrderRepo.Setup(o => o.GetAllOrdersBySitterId(userId)).ReturnsAsync(allOrdersEntities);
            _mockUserRepo.Setup(u => u.GetUserRoleById(userRoleId)).ReturnsAsync(userRoleEntity);

            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.GetAllOrdersUnderConsiderationBySitterId(userId));

            _mockUserRepo.Verify(u => u.GetUserWithRoleById(userId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetAllOrdersBySitterId(userId), Times.Once);
            _mockUserRepo.Verify(u => u.GetUserRoleById(userRoleId), Times.Once);
        }

        private bool Compare(OrderEntity o, OrderEntity orderEntity)
        {
            try
            {
                o.Should().BeEquivalentTo(orderEntity, option => option
                .For(oe => oe.Pets).Exclude(p => p.User)
                .For(oe => oe.Pets).Exclude(p => p.Type)
                .For(oe => oe.Pets).Exclude(p => p.Orders)
                .For(oe => oe.Pets).Exclude(p => p.User.UserRole)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
