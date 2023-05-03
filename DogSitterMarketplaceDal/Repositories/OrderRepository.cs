using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DogSitterMarketplaceDal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger _logger;

        public OrderRepository(OrdersAndPetsAndCommentsContext context, ILogger nLogger)
        {
            _context = context;
            _logger = nLogger;
        }

        public async Task<OrderEntity> AddNewOrder(OrderEntity order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return await _context.Orders
                    .Include(o => o.OrderStatus)
                    .Include(o => o.SitterWork)
                    .Include(o => o.SitterWork.User)
                    .ThenInclude(u => u.UserRole)
                    .Include(o => o.SitterWork.WorkType)
                    .Include(o => o.Location)
                    .SingleAsync(o => o.Id == order.Id);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(AddNewOrder)}");
                throw new ArgumentException();
            }
        }

        public async Task<OrderEntity> ChangeOrderStatus(int orderId, int orderStatusId)
        {
            var orderDB = await _context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
            if (orderDB == null)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderEntity)} with id {orderId} not found.");
                throw new NotFoundException(orderId, nameof(OrderEntity));
            }

            var orderStatusDB = await _context.OrderStatuses.SingleOrDefaultAsync(os => os.Id == orderStatusId);
            if (orderStatusDB == null)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatusEntity)} with id {orderStatusId} not found.");
                throw new NotFoundException(orderStatusId, nameof(OrderStatusEntity));
            }

            orderDB.OrderStatusId = orderStatusId;
            await _context.SaveChangesAsync();

            orderDB = await _context.Orders
                .Include(o => o.OrderStatus)
                .SingleOrDefaultAsync(o => o.Id == orderId);

            return orderDB;
        }

        public async Task<List<OrderEntity>> GetAllOrders()
        {
            return await _context.Orders
                        .Include(o => o.OrderStatus)
                        .Include(o => o.SitterWork)
                        .Include(o => o.Location)
                        .Include(o => o.SitterWork.User)
                        .Include(o => o.SitterWork.WorkType)
                        .Include(o => o.Comments)
                        .Include(o => o.Appeals)
                        .Include(o => o.Pets)
                        .AsNoTracking().ToListAsync();
        }

        public async Task<List<OrderEntity>> GetAllOrdersBySitterId(int userId)
        {
            try
            {
                return await _context.Orders
                        .Include(o => o.OrderStatus)
                        .Include(o => o.SitterWork)
                        .ThenInclude(sw => sw.WorkType)
                        .Include(o => o.Location)
                        .Include(o => o.Pets)
                        .ThenInclude(p => p.User)
                        .Where(o => o.SitterWork.UserId == userId).ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(UserEntity)} with id {userId} not found.");
                throw new NotFoundException(userId, nameof(UserEntity));
            }
        }

        public async Task<OrderEntity> GetOrderById(int id)
        {
            try
            {
                return await _context.Orders
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
                    .SingleAsync(o => o.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public async Task<OrderEntity> UpdateOrder(OrderEntity orderUpdateEntity)
        {
            var orderDB = await _context.Orders
                            .Include(o => o.Pets)
                            .SingleOrDefaultAsync(o => o.Id == orderUpdateEntity.Id && !o.IsDeleted);

            if (orderDB == null)
            {
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

            await _context.SaveChangesAsync();

            return await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.SitterWork)
                .Include(o => o.Location)
                .Include(o => o.Pets)
                .SingleAsync(o => o.Id == orderUpdateEntity.Id);
        }

        public async Task DeleteOrderById(int id)
        {
            try
            {
                var orderDb = await _context.Orders.SingleAsync(o => o.Id == id && !o.IsDeleted);
                orderDb.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public async Task<OrderStatusEntity> GetOrderStatusById(int id)
        {
            try
            {
                return await _context.OrderStatuses.SingleAsync(o => o.Id == id); ;
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatusEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderStatusEntity));
            }
        }

        public async Task<List<OrderEntity>> GetOrdersAtWorkOnDateByUserId(int sitterId, DateTime startDate)
        {
            try
            {
                return await _context.Orders
                                    .Include(o => o.SitterWork)
                                    .Include(o => o.OrderStatus)
                                    .Where(o => o.OrderStatus.Name == OrderStatus.AtWork
                                            && o.DateStart.Date == startDate.Date
                                            && o.SitterWork.UserId == sitterId)
                                    .ToListAsync();
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(UserEntity)} with id {sitterId} not found.");
                throw new NotFoundException(sitterId, nameof(UserEntity));
            }
        }

        public async Task<OrderStatusEntity> GetOrderStatusByName(string name)
        {
            try
            {
                return await _context.OrderStatuses.SingleAsync(os => os.Name == name);
            }
            catch (ArgumentException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatus)} with name {name} not found.");
                throw new ArgumentException($"{nameof(OrderStatus)} with name {name} not found.");
            }
        }

        public async Task<List<OrderEntity>> GetOrdersBySitterIdAndClientId(int sitterId, int clientId)
        {
            try
            {
                return await _context.Orders
                                .Include(o => o.Pets)
                                .Include(o => o.SitterWork)
                                .Where(o => o.SitterWork.UserId == sitterId
                                        && o.Pets.Any(p => p.UserId == clientId))
                                .ToListAsync();
            }
            catch (ArgumentException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderRepository)} {nameof(OrderEntity)} {nameof(GetOrdersBySitterIdAndClientId)}");
                throw new ArgumentException($"{nameof(UserEntity)} with id {sitterId} or {clientId} not found.");
            }
        }
    }
}
