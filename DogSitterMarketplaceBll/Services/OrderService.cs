using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using DogSitterMarketplaceDal.Repositories;
using Microsoft.Data.SqlClient;
using NLog;
using System.Linq;
using System.Text;

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
            var allPets = _petReposotory.GetPetsInOrderEntities(newOrder.Pets);
            var petsNotDeleted = allPets.Where(p => !p.IsDeleted).ToList();

            if (petsNotDeleted.Count <= 0)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Order does not contain existing pets");
                throw new ArgumentException("Order does not contain existing pets");
            }

            if (!CheckAllPetsBelongToSameUser(petsNotDeleted))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Order contains pets with different Users");
                throw new ArgumentException("Order contains pets with different Users");
            }

            if (!CheckDateStartOrderEarlierThenDateEndOrder(newOrder.DateStart, newOrder.DateEnd))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Sitter does not work at dateTime which is in order");
                throw new ArgumentException("DateStart of order should be earlier then dateEnd of order");
            }

            var sitterWork = _orderReposotory.GetSitterWorkById(newOrder.SitterWorkId);

            if (!CheckSitterHasTimingToOrder(sitterWork.UserId, newOrder.DateStart, newOrder.DateEnd, newOrder.LocationId))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Sitter does not work at dateTime which is in order");
                throw new ArgumentException("Sitter does not work at dateTime which is in order");
            }

            if (!CheckSitterIsFreeToNewOrder(sitterWork.UserId, newOrder.DateStart, newOrder.DateEnd))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Sitter already has other order at work on the same(or near) time");
                throw new ArgumentException("Sitter already has other order at work on the same(or near) time");
            }

            var messages = allPets.Where(p => p.IsDeleted).Select(p => $"Pet with id {p.Id} is deleted.");
            var orderEntity = _mapper.Map<OrderEntity>(newOrder);
            orderEntity.Pets.AddRange(petsNotDeleted);
            orderEntity.OrderStatusId = 4;
            var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);
            var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);
            CheckPetsInOrderIsExist(allPets, newOrder.Pets, addOrderResponse);
            addOrderResponse.Messages.AddRange(messages);
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(AddOrder)}");

            return addOrderResponse;
        }

        public OrderResponse ChangeOrderStatusToAtWork(int orderId)
        {
            var orderEntity = _orderReposotory.GetOrderById(orderId);
            if (orderEntity.IsDeleted)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(ChangeOrderStatusToAtWork)} {nameof(OrderEntity)} with id {orderId} is deleted.");
                throw new NotFoundException(orderId, nameof(orderEntity));
            }

            if (orderEntity.OrderStatusId == 3 || orderEntity.OrderStatusId == 6)
            {
                var updateOrderEntity = _orderReposotory.ChangeOrderStatusToAtWork(orderId);
                var orderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);

                return orderResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(ChangeOrderStatusToAtWork)} {nameof(OrderEntity)} Order with id {orderId} has an unsuitable status for changing the status to AtWork");
                throw new ArgumentException($"Order with id {orderId} has an unsuitable status for changing the status to AtWork");
            }
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

        private bool CheckAllPetsBelongToSameUser(List<PetEntity> petsNotDeleted)
        {
            if (petsNotDeleted.Count > 1)
            {
                int id = petsNotDeleted[0].UserId;
                foreach (var pet in petsNotDeleted)
                {
                    if (pet.UserId != id)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckSitterHasTimingToOrder(int sitterId, DateTime startOrder, DateTime endOrder, int locationId)
        {
            try
            {
                var allSitterWorks = _orderReposotory.GetAllSitterWorksByUserId(sitterId);
                if (allSitterWorks.Any())
                {
                    foreach (var sitterWork in allSitterWorks)
                    {
                        foreach (var locationWork in sitterWork.LocationWork)
                        {
                            if (locationWork.LocationId == locationId)
                            {
                                if (startOrder.Date == endOrder.Date)
                                {
                                    foreach (var timingsLocationsWorks in locationWork.TimingLocationWorks)
                                    {
                                        if (startOrder.DayOfWeek.ToString() == timingsLocationsWorks.DayOfWeek.Name
                                            && startOrder.TimeOfDay >= timingsLocationsWorks.Start
                                            && endOrder.TimeOfDay <= timingsLocationsWorks.Stop)
                                        {
                                            return true;
                                        }
                                    }
                                }
                                else
                                {
                                    bool match = false;
                                    foreach (var timingsLocationsWorks in locationWork.TimingLocationWorks)
                                    {
                                        if (startOrder.DayOfWeek.ToString() == timingsLocationsWorks.DayOfWeek.Name
                                            && startOrder.TimeOfDay >= timingsLocationsWorks.Start
                                            && new TimeSpan(23, 59, 00) <= timingsLocationsWorks.Stop
                                            && timingsLocationsWorks.Stop <= new TimeSpan(23, 59, 59))
                                        {
                                            match = true;
                                            break;
                                        }
                                    }

                                    if (match)
                                    {
                                        foreach (var timingsLocationsWorks in locationWork.TimingLocationWorks)
                                        {
                                            if (endOrder.DayOfWeek.ToString() == timingsLocationsWorks.DayOfWeek.Name
                                                && endOrder.TimeOfDay <= timingsLocationsWorks.Stop
                                                && new TimeSpan(00, 00, 00) <= timingsLocationsWorks.Start
                                                && timingsLocationsWorks.Start <= new TimeSpan(00, 00, 59))
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return false;
            }
            catch (NotFoundException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(UserEntity)} with id {sitterId} not found.");
                throw new NotFoundException(sitterId, nameof(UserEntity));
            }
        }

        private bool CheckSitterIsFreeToNewOrder(int sitterId, DateTime startOrder, DateTime endOrder)
        {
            var allOrdersBySitter = _orderReposotory.GetOrdersAtWorkOnDateByUserId(sitterId, startOrder);
            var notDeletedOrders = allOrdersBySitter.Where(o => !o.IsDeleted);
            foreach (var order in notDeletedOrders)
            {
                if (startOrder < order.DateStart && endOrder.AddMinutes(30) > order.DateStart
                    || startOrder < order.DateEnd.AddMinutes(30) && endOrder > order.DateEnd
                    || startOrder > order.DateStart && endOrder < order.DateEnd
                    || startOrder < order.DateStart && endOrder > order.DateEnd)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckDateStartOrderEarlierThenDateEndOrder(DateTime startOrder, DateTime endOrder)
        {
            if (startOrder < endOrder)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}