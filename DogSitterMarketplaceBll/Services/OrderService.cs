using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using DogSitterMarketplaceDal.Repositories;
using NLog;
using System.Linq;

namespace DogSitterMarketplaceBll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderReposotory;

        private readonly IPetRepository _petReposotory;

        private readonly ILogger _logger;

        public OrderService(IOrderRepository orderReposotory, IPetRepository petReposotory, IMapper mapper, ILogger nLogger)
        {
            _orderReposotory = orderReposotory;
            _petReposotory = petReposotory;
            _mapper = mapper;
            _logger = nLogger;
        }

        public OrderResponse AddOrder(OrderCreateRequest newOrder)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(AddOrder)}");
            var orderEntity = _mapper.Map<OrderEntity>(newOrder);
            //orderEntity.Pets.AddRange(_petReposotory.GetPetsInOrderEntities(newOrder.Pets));
            //var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);
            //var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);

            //return addOrderResponse;

            var allPets = _petReposotory.GetPetsInOrderEntities(newOrder.Pets);
            var messages = allPets.Where(p => p.IsDeleted).Select(p => $"Pet with id {p.Id} is deleted.");
            orderEntity.Pets.AddRange(allPets.Where(p => !p.IsDeleted));
            var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);
             var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);
            CheckPetsInOrderIsExist(allPets, newOrder.Pets, addOrderResponse);
            addOrderResponse.Messages.AddRange(messages);
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(AddOrder)}");

            return addOrderResponse;
        }

        public List<OrderResponse> GetAllNotDeletedOrders()
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(GetAllNotDeletedOrders)}");
            var allOrdersEntity = _orderReposotory.GetAllOrders();
            var ordersEntity = allOrdersEntity
                .Where(o => !o.IsDeleted)
                .Select(o =>
                {
                    var order = new OrderEntity
                    {
                        Id = o.Id,
                        Comment = o.Comment,
                        OrderStatus = o.OrderStatus,
                        OrderStatusId = o.OrderStatusId,
                        SitterWork = o.SitterWork,
                        SitterWorkId = o.SitterWorkId,
                        Summ = o.Summ,
                        DateStart = o.DateStart,
                        DateEnd = o.DateEnd,
                        Location = o.Location,
                        LocationId = o.LocationId,
                        IsDeleted = o.IsDeleted,
                        Comments = o.Comments.Where(c => !c.IsDeleted).ToList(),
                        Appeals = o.Appeals.Where(a => !a.IsDeleted).ToList()
                    };
                    order.Pets.Clear();
                    order.Pets.AddRange(o.Pets);
                    return order;
                });

            var ordersResponse = _mapper.Map<List<OrderResponse>>(ordersEntity);
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(GetAllNotDeletedOrders)}");

            return ordersResponse;
        }

        public OrderResponse GetNotDeletedOrderById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(GetNotDeletedOrderById)}");
            var orderEntity = _orderReposotory.GetOrderById(id);

            if (!orderEntity.IsDeleted)
            {
                orderEntity.Comments = orderEntity.Comments.Where(c => !c.IsDeleted).ToList();
                orderEntity.Appeals = orderEntity.Appeals.Where(c => !c.IsDeleted).ToList();
                var orderResponse = _mapper.Map<OrderResponse>(orderEntity);
                _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(GetNotDeletedOrderById)}");

                return orderResponse;
            }
            else
            {
                // _logger.LogDebug($"{nameof(OrderService)} {nameof(GetNotDeletedOrderById)} {nameof(OrderEntity)} with id {id} is deleted.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(GetNotDeletedOrderById)} {nameof(OrderEntity)} with id {id} is deleted.");
                throw new NotFoundException(id, nameof(orderEntity));
            }
        }

        public void DeleteOrderById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(DeleteOrderById)}");
            _orderReposotory.DeleteOrderById(id);
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(DeleteOrderById)}");
        }

        public OrderResponse UpdateOrder(OrderUpdate orderUpdate)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(UpdateOrder)}");
            var orderEntity = _mapper.Map<OrderEntity>(orderUpdate);
            orderEntity.OrderStatus = _orderReposotory.GetOrderStatusById(orderUpdate.OrderStatusId);

            //перенести методы-проверки в Сервис
            orderEntity.SitterWork = _orderReposotory.GetSitterWorkById(orderUpdate.SitterWorkId);
            orderEntity.Location = _orderReposotory.GetLocationById(orderUpdate.LocationId);

            var allPets = _petReposotory.GetPetsInOrderEntities(orderUpdate.Pets);
            var messages = allPets.Where(p => p.IsDeleted).Select(p => $"Pet with id {p.Id} is deleted.");
            orderEntity.Pets.AddRange(allPets.Where(p => !p.IsDeleted));
            var updateOrderEntity = _orderReposotory.UpdateOrder(orderEntity);
            var updateOrderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);
            CheckPetsInOrderIsExist(allPets, orderUpdate.Pets, updateOrderResponse);
            updateOrderResponse.Messages.AddRange(messages);
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(UpdateOrder)}");

            return updateOrderResponse;

        }

        private void CheckPetsInOrderIsExist(List<PetEntity> allPets, List<int> petsId, OrderResponse orderResponse)
        {
            if (allPets.Count != petsId.Count)
            {
                foreach (int petId in petsId)
                {
                    var match = allPets.FirstOrDefault(p => p.Id == petId);
                    if (match == null)
                    {
                        orderResponse.Messages.Add($"Pet with id {petId} not found now.");
                    }
                }
            }
        }
    }
}