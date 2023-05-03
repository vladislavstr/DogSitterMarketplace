using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Repositories;
using NLog;
using System.Collections.Generic;

namespace DogSitterMarketplaceBll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderRepository;

        private readonly IPetRepository _petRepository;

        private readonly IUserRepository _userRepository;

        private readonly IWorkAndLocationRepository _workAndLocationRepository;

        private readonly ILogger _logger;

        public OrderService(IOrderRepository orderReposotory, IPetRepository petReposotory, IUserRepository userRepository, IWorkAndLocationRepository workAndLocationRepository, IMapper mapper, ILogger nLogger)
        {
            _orderRepository = orderReposotory;
            _petRepository = petReposotory;
            _userRepository = userRepository;
            _workAndLocationRepository = workAndLocationRepository;
            _mapper = mapper;
            _logger = nLogger;
        }

        public async Task<OrderResponse> AddOrder(OrderCreateRequest newOrder)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(AddOrder)}");

            var allPets = await _petRepository.GetPetsInOrderEntities(newOrder.Pets);
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

            var sitterWork = await _workAndLocationRepository.GetNotDeletedSitterWorkById(newOrder.SitterWorkId);

            if (sitterWork.LocationWork == null)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)},LocationWork is null");
                throw new ArgumentException("LocationWork is null");
            }
            var summ = sitterWork.LocationWork.SingleOrDefault(lw => lw.SitterWorkId == newOrder.SitterWorkId && lw.LocationId == newOrder.LocationId)?.Price;

            if (!summ.HasValue || summ == 0)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Summ of order can not be null or 0");
                throw new ArgumentException("Summ of order can not be null or 0");
            }

            if (!await CheckSitterHasTimingToOrder(sitterWork.UserId, newOrder.DateStart, newOrder.DateEnd, newOrder.LocationId))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Sitter does not work at dateTime which is in order");
                throw new ArgumentException("Sitter does not work at dateTime which is in order");
            }

            if (!await CheckSitterIsFreeToNewOrder(sitterWork.UserId, newOrder.DateStart, newOrder.DateEnd))
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddOrder)}, Sitter already has other order at work on the same(or near) time");
                throw new ArgumentException("Sitter already has other order at work on the same(or near) time");
            }

            var messagesAboutDeleted = allPets.Where(p => p.IsDeleted).Select(p => $"Pet with id {p.Id} is deleted.");
            var messagesAboutExist = CheckPetsInOrderIsExistAndGetMessages(allPets, newOrder.Pets);

            var orderEntity = _mapper.Map<OrderEntity>(newOrder);
            orderEntity.Pets.AddRange(petsNotDeleted);
            var orderStatusUnderConsideration = await _orderRepository.GetOrderStatusByName(OrderStatus.UnderConsideration);
            orderEntity.OrderStatusId = orderStatusUnderConsideration.Id;
            orderEntity.Summ = summ.Value;

            var addOrderEntity = await _orderRepository.AddNewOrder(orderEntity);
            var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);

            addOrderResponse.Messages.AddRange(messagesAboutDeleted);
            addOrderResponse.Messages.AddRange(messagesAboutExist);

            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(AddOrder)}");

            return addOrderResponse;
        }

        public async Task<OrderResponse> ChangeOrderStatus(int orderId, int orderStatusId)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(ChangeOrderStatus)}");

            var orderResponse = await CheckAndGetOrderIsExistAndIsNotDeleted(orderId);
            var changeOrderResponse = new OrderResponse();
            var orderStatus = await _orderRepository.GetOrderStatusById(orderStatusId);
            switch (orderStatus.Name)
            {
                case OrderStatus.AtWork:
                    changeOrderResponse = await ChangeOrderStatusToAtWork(orderResponse, orderStatusId);
                    break;

                case OrderStatus.Completed:
                    changeOrderResponse = await ChangeOrderStatusToComplete(orderResponse, orderStatusId);
                    break;

                case OrderStatus.Rejected:
                    changeOrderResponse = await ChangeOrderStatusToReject(orderResponse, orderStatusId);
                    break;

                default:
                    _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(ChangeOrderStatus)} You can not change orderStatus to {orderStatusId}");
                    throw new ArgumentException($"You can not change orderStatus to {orderStatusId}");
            }

            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(ChangeOrderStatus)}");

            return changeOrderResponse;
        }

        public async Task<List<OrderResponse>> GetAllOrdersUnderConsiderationBySitterId(int userId)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(GetAllOrdersUnderConsiderationBySitterId)}");

            var userEntity = await _userRepository.GetUserWithRoleById(userId);
            var allOrdersEntities = await _orderRepository.GetAllOrdersBySitterId(userId);
            var userRole = await _userRepository.GetUserRoleById(userEntity.UserRoleId);

            if (userRole.Name == UserRole.Sitter)
            {
                var ordersUnderConsiderationEntities = allOrdersEntities.Where(o => o.OrderStatus.Name == OrderStatus.UnderConsideration).ToList();
                var ordersUnderConsiderationResponses = _mapper.Map<List<OrderResponse>>(ordersUnderConsiderationEntities);
                _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(GetAllOrdersUnderConsiderationBySitterId)}");

                return ordersUnderConsiderationResponses;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(GetAllOrdersUnderConsiderationBySitterId)} User with id {userId} has not got necessary UserRole");
                throw new ArgumentException($"User with id {userId} has not got necessary UserRole");
            }
        }

        public async Task<List<OrderResponse>> GetAllNotDeletedOrders()
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(GetAllNotDeletedOrders)}");

            var allOrdersEntity = await _orderRepository.GetAllOrders();
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

        public async Task<OrderResponse> GetNotDeletedOrderById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(GetNotDeletedOrderById)}");

            var orderEntity = await _orderRepository.GetOrderById(id);

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
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(GetNotDeletedOrderById)} {nameof(OrderEntity)} with id {id} is deleted.");
                throw new NotFoundException(id, nameof(orderEntity));
            }
        }

        public async Task DeleteOrderById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(DeleteOrderById)}");

            await _orderRepository.DeleteOrderById(id);

            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(DeleteOrderById)}");
        }

        public async Task<OrderResponse> UpdateOrder(OrderUpdate orderUpdate)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(UpdateOrder)}");

            if (await CheckAndGetOrderIsExistAndIsNotDeleted(orderUpdate.Id) == null)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(UpdateOrder)} {nameof(OrderEntity)} with id {orderUpdate.Id} is not exist.");
                throw new NotFoundException(orderUpdate.Id, nameof(OrderEntity));
            }

            var allPets = await _petRepository.GetPetsInOrderEntities(orderUpdate.Pets);
            var petsNotDeleted = allPets.Where(p => !p.IsDeleted).ToList();

            if (petsNotDeleted.Count <= 0)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(UpdateOrder)}, Order does not contain existing pets");
                throw new ArgumentException("Order does not contain existing pets");
            }

            var messagesAboutDeleted = allPets.Where(p => p.IsDeleted).Select(p => $"Pet with id {p.Id} is deleted.");
            var messagesAboutExist = CheckPetsInOrderIsExistAndGetMessages(allPets, orderUpdate.Pets);

            var orderEntity = _mapper.Map<OrderEntity>(orderUpdate);
            orderEntity.SitterWork = await _workAndLocationRepository.GetNotDeletedSitterWorkById(orderUpdate.SitterWorkId);
            orderEntity.Location = await _workAndLocationRepository.GetLocationById(orderUpdate.LocationId);
            orderEntity.Pets.AddRange(allPets.Where(p => !p.IsDeleted));

            var updateOrderEntity = await _orderRepository.UpdateOrder(orderEntity);
            var updateOrderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);

            updateOrderResponse.Messages.AddRange(messagesAboutDeleted);
            updateOrderResponse.Messages.AddRange(messagesAboutExist);

            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(UpdateOrder)}");

            return updateOrderResponse;
        }

        public async Task<OrderResponse> CheckAndGetOrderIsExistAndIsNotDeleted(int orderId)
        {
            var orderEntity = await _orderRepository.GetOrderById(orderId);
            if (orderEntity.IsDeleted)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(CommentService)} {nameof(CheckAndGetOrderIsExistAndIsNotDeleted)} {nameof(OrderEntity)} with id {orderId} is deleted.");
                throw new NotFoundException(orderId, nameof(OrderEntity));
            }
            var orderResponse = _mapper.Map<OrderResponse>(orderEntity);

            return orderResponse;
        }

        public async Task<List<OrderResponse>> AddSeveralOrdersForOneClientFromOneSitter(List<OrderCreateRequest> orders)
        {
            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} start {nameof(AddSeveralOrdersForOneClientFromOneSitter)}");

            if (orders.Count < 1)
            {
                return new List<OrderResponse>();
            }
            else if (orders.Count == 1)
            {
                var orderResponse = await AddOrder(orders.Single());
                return new List<OrderResponse> { orderResponse };
            }
            else
            {
                if (!await CheckSeveralSitterWorksWithSameSitter(orders))
                {
                    _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(AddSeveralOrdersForOneClientFromOneSitter)} You can add several orders with the same Sitter, but request List contains different sitters");
                    throw new ArgumentException("You can add several orders with the same Sitter, but request List contains different sitters");
                }
                else
                {
                    if (!await CheckSeveralPetsBelongToSameClient(orders))
                    {
                        _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(AddSeveralOrdersForOneClientFromOneSitter)} You can add several orders where pets belong to the same Client, but request List contains different clients");
                        throw new ArgumentException("You can add several orders where pets belong to the same Client, but request List contains different clients");
                    }
                    else
                    {
                        if (CheckOrdersAreNotCrossedEachOther(orders))
                        {
                            List<OrderResponse> result = new List<OrderResponse>();

                            foreach (var order in orders)
                            {
                                var addOrderResponse = await AddOrder(order);
                                result.Add(addOrderResponse);
                            }

                            _logger.Log(LogLevel.Info, $"{nameof(OrderService)} end {nameof(AddSeveralOrdersForOneClientFromOneSitter)}");

                            return result;
                        }
                        else
                        {
                            _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(AddSeveralOrdersForOneClientFromOneSitter)} DataTime one of the orders cross DataTime other order");
                            throw new ArgumentException("DataTime one of the orders cross DataTime other order");
                        }
                    }
                }
            }
        }

        private List<string> CheckPetsInOrderIsExistAndGetMessages(List<PetEntity> allPets, List<int> petsId)
        {
            List<string> messages = new List<string>();

            if (allPets.Count != petsId.Count)
            {
                foreach (int petId in petsId)
                {
                    var match = allPets.FirstOrDefault(p => p.Id == petId);
                    if (match == null)
                    {
                        messages.Add($"Pet with id {petId} not found now.");
                    }                    
                }
            }

            return messages;
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

        private async Task<bool> CheckSitterHasTimingToOrder(int sitterId, DateTime startOrder, DateTime endOrder, int locationId)
        {
            try
            {
                var allSitterWorks = await _workAndLocationRepository.GetAllSitterWorksByUserId(sitterId);
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

        private async Task<bool> CheckSitterIsFreeToNewOrder(int sitterId, DateTime startOrder, DateTime endOrder)
        {
            var allOrdersBySitter = await _orderRepository.GetOrdersAtWorkOnDateByUserId(sitterId, startOrder);
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

        private async Task<OrderResponse> ChangeOrderStatusToAtWork(OrderResponse orderResponse, int orderStatusId)
        {
            if (orderResponse.OrderStatus.Name == OrderStatus.UnderConsideration || orderResponse.OrderStatus.Name == OrderStatus.Rejected)
            {
                var updateOrderEntity = await _orderRepository.ChangeOrderStatus(orderResponse.Id, orderStatusId);
                var changeOrderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);

                return changeOrderResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(ChangeOrderStatus)} {nameof(OrderEntity)} Order with id {orderResponse.Id} has an unsuitable status for changing the status to AtWork");
                throw new ArgumentException($"Order with id {orderResponse.Id} has an unsuitable status for changing the status to AtWork");
            }
        }

        private async Task<OrderResponse> ChangeOrderStatusToReject(OrderResponse orderResponse, int orderStatusId)
        {
            if (orderResponse.OrderStatus.Name == OrderStatus.UnderConsideration)
            {
                var updateOrderEntity = await _orderRepository.ChangeOrderStatus(orderResponse.Id, orderStatusId);
                var changeOrderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);

                return changeOrderResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(ChangeOrderStatus)} {nameof(OrderEntity)} Order with id {orderResponse.Id} has an unsuitable status for changing the status to Reject");
                throw new ArgumentException($"Order with id {orderResponse.Id} has an unsuitable status for changing the status to Reject");
            }
        }

        private async Task<OrderResponse> ChangeOrderStatusToComplete(OrderResponse orderResponse, int orderStatusId)
        {
            if (orderResponse.OrderStatus.Name == OrderStatus.AtWork)
            {
                var updateOrderEntity = await _orderRepository.ChangeOrderStatus(orderResponse.Id, orderStatusId);
                var changeOrderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);

                return changeOrderResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(ChangeOrderStatus)} {nameof(OrderEntity)} Order with id {orderResponse.Id} has an unsuitable status for changing the status to Reject");
                throw new ArgumentException($"Order with id {orderResponse.Id} has an unsuitable status for changing the status to Reject");
            }
        }

        private bool CheckOrdersAreNotCrossedEachOther(List<OrderCreateRequest> orders)
        {
            var sortedOrders = orders.OrderBy(o => o.DateStart).ToArray();
            for (int i = 0; i < sortedOrders.Length - 1; i++)
            {
                if (sortedOrders[i].DateEnd >= sortedOrders[i + 1].DateStart)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> CheckSeveralSitterWorksWithSameSitter(List<OrderCreateRequest> orders)
        {
            var sittersWorksId = orders.Select(o => o.SitterWorkId).Distinct().ToList();
            var sittersWorks = await _workAndLocationRepository.GetSittersWorksByThemId(sittersWorksId);
            var groupSittersWorksCount = sittersWorks.GroupBy(sw => sw.UserId).Count();

            if (groupSittersWorksCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            };
        }

        private async Task<bool> CheckSeveralPetsBelongToSameClient(List<OrderCreateRequest> orders)
        {
            var petsId = orders.SelectMany(o => o.Pets).ToList();
            var petsEntity = await _petRepository.GetPetsInOrderEntities(petsId);

            return CheckAllPetsBelongToSameUser(petsEntity);
        }
    }
}

