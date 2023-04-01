using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderReposotory;

        public OrderService(IOrderRepository orderReposotory)
        {
            _orderReposotory = orderReposotory;
        }

        public List<OrderResponse> GetAllOrders()
        {
            var ordersEntity = _orderReposotory.GetAllOrders();
            return ordersEntity.Select(e =>
            {
                var orderResponse = new OrderResponse
                {
                    Id = e.Id,
                    Comment = e.Comment
                };
                if (e.OrderStatus != null)
                {
                    orderResponse.OrderStatus = new OrderStatusResponse
                    {
                        Id = e.OrderStatus.Id,
                        Comment = e.OrderStatus.Comment,
                    };
                }

                return orderResponse;
            }).ToList();
        }

        public OrderResponse GetOrderById(int id)
        {
            var orderEntity = _orderReposotory.GetOrderById(id);
            return new OrderResponse
            {
                Id = orderEntity.Id
            };
        }

        public OrderResponse AddOrder(OrderRequest newOrder)
        {
            var orderEntity = new OrderEntity
            {
                Location = new LocationEntity
                {
                    Id = newOrder.LocationId
                },
                Summ = newOrder.Summ
            };
            var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);

            return new OrderResponse
            {
                Id = addOrderEntity.Id
            };
        }
    }
}