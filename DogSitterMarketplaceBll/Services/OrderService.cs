using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;
using DogSitterMarketplaceDal.Repositories;

namespace DogSitterMarketplaceBll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderReposotory;

        private readonly IPetRepository _petReposotory;

        public OrderService(IOrderRepository orderReposotory, IPetRepository petReposotory, IMapper mapper)
        {
            _orderReposotory = orderReposotory;
            _petReposotory = petReposotory;
            _mapper = mapper;
        }

        public OrderResponse AddOrder(OrderCreateRequest newOrder)
        {
            var orderEntity = _mapper.Map<OrderEntity>(newOrder);

            //orderEntity.OrderStatus = _orderReposotory.GetOrderStatusById(newOrder.OrderStatusId);
            //orderEntity.SitterWork = _orderReposotory.GetSitterWorkById(newOrder.SitterWorkId);
            //orderEntity.Location = _orderReposotory.GetLocationById(newOrder.LocationId);

            orderEntity.Pets.AddRange(_petReposotory.GetPetsInOrderEntities(newOrder.Pets));

            //orderEntity.Pets.AddRange(_orderReposotory.GetPetsInOrderEntities(newOrder.Pets));

            var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);
            var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);

            return addOrderResponse;
        }

        public List<OrderResponse> GetAllOrders()
        {
            var ordersEntity = _orderReposotory.GetAllOrders();
            var ordersResponse = _mapper.Map<List<OrderResponse>>(ordersEntity);

            return ordersResponse;
        }

        public OrderResponse GetOrderById(int id)
        {
            var orderEntity = _orderReposotory.GetOrderById(id);
            var orderResponse = _mapper.Map<OrderResponse>(orderEntity);

            return orderResponse;
        }

        public void DeleteOrderById(int id)
        {
            _orderReposotory.DeleteOrderById(id);
        }

        public int UpdateOrder(OrderUpdate orderUpdate)
        {
            var orderEntity = _mapper.Map<OrderEntity>(orderUpdate);
            orderEntity.OrderStatus = _orderReposotory.GetOrderStatusById(orderUpdate.OrderStatusId);
            orderEntity.SitterWork = _orderReposotory.GetSitterWorkById(orderUpdate.SitterWorkId);
            orderEntity.Location = _orderReposotory.GetLocationById(orderUpdate.LocationId);
            orderEntity.Pets.AddRange(_orderReposotory.GetPetsInOrderEntities(orderUpdate.Pets));
                        
            _orderReposotory.UpdateOrder(orderEntity);

            return orderEntity.Id;
        }
    }
}