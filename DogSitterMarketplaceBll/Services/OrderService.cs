using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderReposotory;

        public OrderService(IOrderRepository orderReposotory, IMapper mapper)
        {
            _orderReposotory = orderReposotory;
            _mapper = mapper;
        }

        public OrderResponse AddOrder(OrderRequest newOrder)
        {
            var orderEntity = _mapper.Map<OrderEntity>(newOrder);
            var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);
            var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);

            return addOrderResponse;

            //var orderEntity = new OrderEntity
            //{
            //    Location = new LocationEntity
            //    {
            //        Id = newOrder.LocationId
            //    },
            //    Summ = newOrder.Summ
            //};
            //var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);

            //return new OrderResponse
            //{
            //    Id = addOrderEntity.Id
            //};
        }

        public List<OrderResponse> GetAllOrders()
        {
            var ordersEntity = _orderReposotory.GetAllOrders();
            var ordersResponse = _mapper.Map<List<OrderResponse>>(ordersEntity);

            return ordersResponse;


            //var ordersEntity = _orderReposotory.GetAllOrders();
            //return ordersEntity.Select(e =>
            //{
            //    var orderResponse = new OrderResponse
            //    {
            //        Id = e.Id,
            //        Comment = e.Comment
            //    };
            //    if (e.OrderStatus != null)
            //    {
            //        orderResponse.OrderStatus = new OrderStatusResponse
            //        {
            //            Id = e.OrderStatus.Id,
            //            Comment = e.OrderStatus.Comment,
            //        };
            //    }

            //    return orderResponse;
            //}).ToList();
        }

        public OrderResponse GetOrderById(int id)
        {
            //var orderEntity = _orderReposotory.GetOrderById(id);
            //return new OrderResponse
            //{
            //    Id = orderEntity.Id
            //};

            var orderEntity = _orderReposotory.GetOrderById(id);
            var orderResponse = _mapper.Map<OrderResponse>(orderEntity);

            return orderResponse;
        }

        public void DeleteOrderById(int id)
        {
            _orderReposotory.DeleteOrderById(id);
        }
    }
}