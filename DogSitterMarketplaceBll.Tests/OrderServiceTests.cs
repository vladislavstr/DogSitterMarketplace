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
            _orderService = new OrderService(
                                            _mockOrderRepo.Object,
                                            _mockPetRepo.Object,
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
                                OrderEntity addOrderEntity, OrderCreateRequest newOrder, OrderResponse expected)
        {
            _mockPetRepo.Setup(p => p.GetPetsInOrderEntities(petsId)).Returns(allPets);
            _mockOrderRepo.Setup(o => o.AddNewOrder(It.Is<OrderEntity>(o => method(o, orderEntity)))).Returns(addOrderEntity);

            OrderResponse actual = _orderService.AddOrder(newOrder);

            _mockPetRepo.Verify((p => p.GetPetsInOrderEntities(petsId)), Times.Once);
            _mockOrderRepo.Verify(o => o.AddNewOrder(It.IsAny<OrderEntity>()), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }

        private bool method(OrderEntity o, OrderEntity orderEntity)
        {
            try
            {
                o.Should().BeEquivalentTo(orderEntity, option => option
                //.Excluding(o => o.Pets)
                .For(oe => oe.Pets).Exclude(p => p.User)
                .For(oe => oe.Pets).Exclude(p => p.Type)
                .For(oe => oe.Pets).Exclude(p => p.Orders)
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
