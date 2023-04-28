﻿using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Linq;

namespace DogSitterMarketplaceDal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        // private readonly ILogger<IOrderRepository> _logger;

        private readonly ILogger _logger;

        public OrderRepository(OrdersAndPetsAndCommentsContext context, ILogger nLogger)
        {
            _context = context;
            _logger = nLogger;
        }

        public OrderEntity AddNewOrder(OrderEntity order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                return _context.Orders
                    .Include(o => o.OrderStatus)
                    .Include(o => o.SitterWork)
                    .Include(o => o.SitterWork.User)
                    .Include(o => o.SitterWork.WorkType)
                    .Include(o => o.Location)
                    .Single(o => o.Id == order.Id);
            }
            catch (Exception ex)
            {
                //_logger.LogDebug($"{ex}, {nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddNewOrder)}");
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddNewOrder)}");
                throw new ArgumentException();
            }
        }

        public OrderEntity ChangeOrderStatus(int orderId, int orderStatusId)
        {
            var orderDB = _context.Orders.SingleOrDefault(o => o.Id == orderId);
            if (orderDB == null)
            {
                // _logger.LogDebug($"{nameof(OrderEntity)} with id {orderUpdateEntity.Id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderEntity)} with id {orderId} not found.");
                throw new NotFoundException(orderId, nameof(OrderEntity));
            }

            var orderStatusDB = _context.OrderStatuses.SingleOrDefault(os => os.Id == orderStatusId);
            if (orderStatusDB == null)
            {
                // _logger.LogDebug($"{nameof(OrderEntity)} with id {orderUpdateEntity.Id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatusEntity)} with id {orderStatusId} not found.");
                throw new NotFoundException(orderStatusId, nameof(OrderStatusEntity));
            }

            orderDB.OrderStatusId = orderStatusId;
            _context.SaveChanges();
            orderDB = _context.Orders
                .Include(o => o.OrderStatus)
                .SingleOrDefault(o => o.Id == orderId);

            return orderDB;
        }

        public List<OrderEntity> GetAllOrders()
        {
            return _context.Orders
                        .Include(o => o.OrderStatus)
                        .Include(o => o.SitterWork)
                        .Include(o => o.Location)
                        .Include(o => o.SitterWork.User)
                        .Include(o => o.SitterWork.WorkType)
                        .Include(o => o.Comments)
                        .Include(o => o.Appeals)
                        .Include(o => o.Pets)
                        .AsNoTracking().ToList();
        }

        public List<OrderEntity> GetAllOrdersBySitterId(int userId)
        {
            try
            {
                return _context.Orders
                        .Include(o => o.OrderStatus)
                        .Include(o => o.SitterWork)
                        .ThenInclude(sw => sw.WorkType)
                        .Include(o => o.Location)
                        .Include(o => o.Pets)
                        .ThenInclude(p => p.User)
                        .Where(o => o.SitterWork.UserId == userId).ToList();
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(OrderEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(UserEntity)} with id {userId} not found.");
                throw new NotFoundException(userId, nameof(UserEntity));
            }
        }

        public OrderEntity GetOrderById(int id)
        {
            try
            {
                return _context.Orders
                    .Include(o => o.OrderStatus)
                    .Include(o => o.SitterWork)
                    .Include(o => o.Location)
                    .Include(o => o.SitterWork.User)
                    .Include(o => o.SitterWork.WorkType)
                    .Include(o => o.Comments)
                    .ThenInclude(o => o.CommentToUser)
                    .Include(o => o.Comments)
                    .ThenInclude(o => o.CommentFromUser)
                    .Include(o => o.Appeals)
                    .ThenInclude(o => o.AppealFromUser)
                    .Include(o => o.Appeals)
                    .ThenInclude(o => o.AppealToUser)
                    .Include(o => o.Pets)
                    .Single(o => o.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(OrderEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public OrderEntity UpdateOrder(OrderEntity orderUpdateEntity)
        {
            // _context.Orders.Update(orderUpdateEntity);

            var orderDB = _context.Orders
                .Include(o => o.Pets)
                .SingleOrDefault(o => o.Id == orderUpdateEntity.Id && !o.IsDeleted);

            if (orderDB == null)
            {
                // _logger.LogDebug($"{nameof(OrderEntity)} with id {orderUpdateEntity.Id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderEntity)} with id {orderUpdateEntity.Id} not found.");
                throw new NotFoundException(orderUpdateEntity.Id, nameof(OrderEntity));
            }

            orderDB.Comment = orderUpdateEntity.Comment;
            orderDB.SitterWorkId = orderUpdateEntity.SitterWorkId;
            orderDB.Summ = orderUpdateEntity.Summ;
            orderDB.DateStart = orderUpdateEntity.DateStart;
            orderDB.DateEnd = orderUpdateEntity.DateEnd;
            orderDB.LocationId = orderUpdateEntity.LocationId;
            orderDB.Pets.Clear();
            orderDB.Pets.AddRange(orderUpdateEntity.Pets);

            _context.SaveChanges();

            return _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.SitterWork)
                .Include(o => o.Location)
                .Include(o => o.Pets)
                .Single(o => o.Id == orderUpdateEntity.Id);
        }

        public void DeleteOrderById(int id)
        {
            try
            {
                var orderDb = _context.Orders.Single(o => o.Id == id && !o.IsDeleted);
                orderDb.IsDeleted = true;
                _context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(OrderEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public OrderStatusEntity GetOrderStatusById(int id)
        {
            try
            {
                return _context.OrderStatuses.Single(o => o.Id == id); ;
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(OrderStatusEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatusEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderStatusEntity));
            }
        }

        public List<OrderEntity> GetOrdersAtWorkOnDateByUserId(int sitterId, DateTime startDate)
        {
            try
            {
                return _context.Orders
                    .Include(o => o.SitterWork)
                    .Include(o => o.OrderStatus)
                    .Where(o => o.OrderStatus.Name == OrderStatus.AtWork
                        && o.DateStart.Date == startDate.Date
                        && o.SitterWork.UserId == sitterId)
                    .ToList();
            }
            catch (InvalidOperationException ex)
            {
                // logger.LogDebug($"{nameof(SitterWorkEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(UserEntity)} with id {sitterId} not found.");
                throw new NotFoundException(sitterId, nameof(UserEntity));
            }
        }

        public OrderStatusEntity GetOrderStatusByName(string name)
        {
            try
            {
                return _context.OrderStatuses.Single(os => os.Name == name);
            }
            catch (ArgumentException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatus)} with name {name} not found.");
                throw new ArgumentException($"{nameof(OrderStatus)} with name {name} not found.");
            }
        }

        public List<OrderEntity> GetOrdersBySitterIdAndClientId(int sitterId, int clientId)
        {
            try
            {
                return _context.Orders
                    .Include(o => o.Pets)
                    .Include(o => o.SitterWork)
                    .Where(o => o.SitterWork.UserId == sitterId
                        && o.Pets.Any(p => p.UserId == clientId))
                    .ToList();
            }
            catch (ArgumentException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(GetOrdersBySitterIdAndClientId)}");
                throw new ArgumentException($"{nameof(UserEntity)} with id {sitterId} or {clientId} not found.");
            }
        }
    }
}
