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
using DogSitterMarketplaceDal.Models.Works;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.Tests
{
    public class OrderServiceTests
    {
        private OrderService _orderService;

        private Mock<IOrderRepository> _mockOrderRepo;

        private Mock<IPetRepository> _mockPetRepo;

        private Mock<IUserRepository> _mockUserRepo;

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
            _orderService = new OrderService(
                                            _mockOrderRepo.Object,
                                            _mockPetRepo.Object,
                                            _mockUserRepo.Object,
                                            _mapper,
                                            logger);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.DeleteOrderByIdTestCaseSource))]
        public void DeleteOrderByIdTest(int id)
        {
            _mockOrderRepo.Setup(o => o.DeleteOrderById(id));

            _orderService.DeleteOrderById(id);

            _mockOrderRepo.Verify(o => o.DeleteOrderById(id), Times.Once);
            _mockOrderRepo.VerifyNoOtherCalls();
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.DeleteOrderByIdTest_WhenIdIsNotExist_ShouldNotFoundExceptionTestCaseSource))]
        public void DeleteOrderByIdTest_WhenIdIsNotExist_ShouldNotFoundException(int id)
        {
            _mockOrderRepo.Setup(o => o.DeleteOrderById(id)).Throws<NotFoundException>(() => new NotFoundException(id, nameof(OrderEntity)));

            Assert.Throws<NotFoundException>(() => _orderService.DeleteOrderById(id));

            _mockOrderRepo.Verify((o => o.DeleteOrderById(id)), Times.Once);
            _mockOrderRepo.VerifyNoOtherCalls();
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrderTestCaseSource))]
        public void AddOrderTest(List<int> petsId, List<PetEntity> allPets, List<string> messagesOfIsDeleted, OrderEntity orderEntity,
                                OrderEntity addOrderEntity, OrderCreateRequest newOrder, OrderResponse expected, int sitterId, SitterWorkEntity sitterWork,
                                List<SitterWorkEntity> allSitterWorks, DateTime startDateOrder, List<OrderEntity> allOrdersBySitter, int sitterWorkId,
                                OrderStatusEntity orderStatusUnderConsideration)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);
            _mockOrderRepo.Setup(o => o.GetSitterWorkById(sitterWorkId)).Returns(sitterWork);
            _mockOrderRepo.Setup(o => o.GetAllSitterWorksByUserId(sitterId)).Returns(allSitterWorks);
            _mockOrderRepo.Setup(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder)).Returns(allOrdersBySitter);
            _mockOrderRepo.Setup(o => o.GetOrderStatusByName("under consideration")).Returns(orderStatusUnderConsideration);
            _mockOrderRepo.Setup(o => o.AddNewOrder(It.Is<OrderEntity>(o => method(o, orderEntity)))).Returns(addOrderEntity);

            OrderResponse actual = _orderService.AddOrder(newOrder);

            _mockPetRepo.Verify((p => p.GetPetsInOrderEntities(petsId)), Times.Once);
            _mockOrderRepo.Verify(o => o.GetSitterWorkById(sitterWorkId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusByName("under consideration"), Times.Once);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenPetIsNotExist_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenPetIsNotExist_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder,
                                                                            int sitterId, DateTime startDateOrder, int sitterWorkId)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);

            Assert.Throws<ArgumentException>(() => _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetSitterWorkById(sitterWorkId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenPetsBelongToDifferentUsers_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenPetsBelongToDifferentUsers_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder,
                                                                            int sitterId, DateTime startDateOrder, int sitterWorkId)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);

            Assert.Throws<ArgumentException>(() => _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetSitterWorkById(sitterWorkId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenDateStartOrderNotEarlierThenDateEndOrder_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenDateStartOrderNotEarlierThenDateEndOrder_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder,
                                                                            int sitterId, DateTime startDateOrder, int sitterWorkId)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);

            Assert.Throws<ArgumentException>(() => _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetSitterWorkById(sitterWorkId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Never);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenSitterHasNotTimingToOrder_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenSitterHasNotTimingToOrder_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder, int sitterId,
                                                                            DateTime startDateOrder, int sitterWorkId, SitterWorkEntity sitterWork, List<SitterWorkEntity> allSitterWorks)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);
            _mockOrderRepo.Setup(o => o.GetSitterWorkById(sitterWorkId)).Returns(sitterWork);
            _mockOrderRepo.Setup(o => o.GetAllSitterWorksByUserId(sitterId)).Returns(allSitterWorks);

            Assert.Throws<ArgumentException>(() => _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetSitterWorkById(sitterWorkId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Never);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.AddOrder_WhenSitterIsNotFreeToNewOrder_ShouldArgumentException_TestCaseSource))]
        public void AddOrderTest_WhenSitterIsNotFreeToNewOrder_ShouldArgumentException(List<int> petsId, List<PetEntity> allPets, OrderCreateRequest newOrder, int sitterId,
                                                                            DateTime startDateOrder, int sitterWorkId, SitterWorkEntity sitterWork, List<SitterWorkEntity> allSitterWorks,
                                                                            List<OrderEntity> allOrdersBySitter)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);
            _mockOrderRepo.Setup(o => o.GetSitterWorkById(sitterWorkId)).Returns(sitterWork);
            _mockOrderRepo.Setup(o => o.GetAllSitterWorksByUserId(sitterId)).Returns(allSitterWorks);
            _mockOrderRepo.Setup(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder)).Returns(allOrdersBySitter);

            Assert.Throws<ArgumentException>(() => _orderService.AddOrder(newOrder));

            _mockPetRepo.Verify(p => p.GetPetsInOrderEntities(petsId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetSitterWorkById(sitterWorkId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetAllSitterWorksByUserId(sitterId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrdersAtWorkOnDateByUserId(sitterId, startDateOrder), Times.Once);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.ChangeOrderStatusTestCaseSource))]
        public void ChangeOrderStatusTest(int orderId, OrderEntity orderEntity, int orderStatusId, OrderStatusEntity orderStatus,
                                          OrderEntity updateOrderEntity, OrderResponse expected)
        {
            _mockOrderRepo.Setup(o => o.GetOrderById(orderId)).Returns(orderEntity);
            _mockOrderRepo.Setup(o => o.GetOrderStatusById(orderStatusId)).Returns(orderStatus);
            _mockOrderRepo.Setup(o => o.ChangeOrderStatus(orderId, orderStatusId)).Returns(updateOrderEntity);

            OrderResponse actual = _orderService.ChangeOrderStatus(orderId, orderStatusId);

            _mockOrderRepo.Verify(o => o.GetOrderById(orderId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusById(orderStatusId), Times.Once);
            _mockOrderRepo.Verify(o => o.ChangeOrderStatus(orderId, orderStatusId), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.ChangeOrderStatus_WhenNewOrderStatusIsNotExist_ShouldBeArgumentException_TestCaseSource))]
        public void ChangeOrderStatusTest_WhenNewOrderStatusIsNotExist_ShouldBeArgumentException(int orderId, OrderEntity orderEntity, int orderStatusId, OrderStatusEntity orderStatus)
        {
            _mockOrderRepo.Setup(o => o.GetOrderById(orderId)).Returns(orderEntity);
            _mockOrderRepo.Setup(o => o.GetOrderStatusById(orderStatusId)).Returns(orderStatus);

            Assert.Throws<ArgumentException>(() => _orderService.ChangeOrderStatus(orderId, orderStatusId)); ;

            _mockOrderRepo.Verify(o => o.GetOrderById(orderId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusById(orderStatusId), Times.Once);
            _mockOrderRepo.Verify(o => o.ChangeOrderStatus(orderId, orderStatusId), Times.Never);
        }

        [TestCaseSource(typeof(OrderServiceTestCaseSource), nameof(OrderServiceTestCaseSource.ChangeOrderStatus_WhenCanNotChangeToAtWork_ShouldBeArgumentException_TestCaseSource))]
        public void ChangeOrderStatusTest_WhenCanNotChangeToAtWork_ShouldBeArgumentException(int orderId, OrderEntity orderEntity, int orderStatusId, OrderStatusEntity orderStatus)
        {
            _mockOrderRepo.Setup(o => o.GetOrderById(orderId)).Returns(orderEntity);
            _mockOrderRepo.Setup(o => o.GetOrderStatusById(orderStatusId)).Returns(orderStatus);

            Assert.Throws<ArgumentException>(() => _orderService.ChangeOrderStatus(orderId, orderStatusId)); ;

            _mockOrderRepo.Verify(o => o.GetOrderById(orderId), Times.Once);
            _mockOrderRepo.Verify(o => o.GetOrderStatusById(orderStatusId), Times.Once);
            _mockOrderRepo.Verify(o => o.ChangeOrderStatus(orderId, orderStatusId), Times.Never);
        }

        private bool method(OrderEntity o, OrderEntity orderEntity)
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
