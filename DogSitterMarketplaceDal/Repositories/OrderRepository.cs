using DogSitterMarketplaceCore.Exceptions;
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

            orderDB.OrderStatusId = orderStatusId;
            _context.SaveChanges();
            orderDB = _context.Orders
                .Include(o => o.OrderStatus)
                .SingleOrDefault(o => o.Id == orderId);

            return orderDB;
        }

        public List<OrderEntity> GetAllOrders()
        {
            //return _context.Orders
            //    .Include(o => o.OrderStatus)
            //    .Include(o => o.SitterWork)
            //    .Include(o => o.Location)
            //    .Include(o => o.SitterWork.User)
            //    .Include(o => o.SitterWork.WorkType)
            //    .Include(o => o.Comments.Where(c => !c.IsDeleted))
            //    .Include(o => o.Appeals.Where(a => !a.IsDeleted))
            //    .Include(o => o.Pets)
            //    .Where(o => !o.IsDeleted
            //           && !o.OrderStatus.IsDeleted
            //           && !o.SitterWork.IsDeleted
            //           && !o.SitterWork.User.IsDeleted
            //           && !o.SitterWork.WorkType.IsDeleted
            //    ).AsNoTracking().ToList();

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
                //return _context.Orders
                //    .Include(o => o.OrderStatus)
                //    .Include(o => o.SitterWork)
                //    .Include(o => o.Location)
                //    .Include(o => o.SitterWork.User)
                //    .Include(o => o.SitterWork.WorkType)
                //    .Include(o => o.Comments)
                //    .ThenInclude(o => o.CommentToUser)
                //    .Include(o => o.Comments)
                //    .ThenInclude(o => o.CommentFromUser)
                //    .Include(o => o.Appeals)
                //    .ThenInclude(o => o.AppealFromUser)
                //    .Include(o => o.Appeals)
                //    .ThenInclude(o => o.AppealToUser)
                //    .Include(o => o.Pets)
                //    .Single(o => o.Id == id && !o.IsDeleted
                //     && !o.OrderStatus.IsDeleted
                //     && !o.SitterWork.IsDeleted
                //     && !o.SitterWork.User.IsDeleted
                //     && !o.SitterWork.WorkType.IsDeleted);

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
            orderDB.OrderStatusId = orderUpdateEntity.OrderStatusId;
            orderDB.SitterWorkId = orderUpdateEntity.SitterWorkId;
            orderDB.Summ = orderUpdateEntity.Summ;
            orderDB.DateStart = orderUpdateEntity.DateStart;
            orderDB.DateEnd = orderUpdateEntity.DateEnd;
            orderDB.LocationId = orderUpdateEntity.LocationId;
            //orderDB.IsDeleted = orderUpdateEntity.IsDeleted;
            //orderDB.Comments = orderUpdateEntity.Comments;
            //orderDB.Appeals = orderUpdateEntity.Appeals;
            //orderDB.Pets.
            orderDB.Pets.Clear();
            orderDB.Pets.AddRange(orderUpdateEntity.Pets);

            _context.SaveChanges();

            return orderDB;
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

        // перенести в Сервис
        public LocationEntity GetLocationById(int id)
        {
            try
            {
                return _context.Location.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(LocationEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(LocationEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(LocationEntity));
            }
        }

        public OrderStatusEntity GetOrderStatusById(int id)
        {
            try
            {
                return _context.OrderStatuses.Single(o => o.Id == id); //&& !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(OrderStatusEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(OrderStatusEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderStatusEntity));
            }
        }

        // перенести в Сервис
        public SitterWorkEntity GetSitterWorkById(int id)
        {
            try
            {
                return _context.SitterWork.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                // logger.LogDebug($"{nameof(SitterWorkEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(SitterWorkEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(SitterWorkEntity));
            }
        }

        // перенести в ЮзерРепозитори
        public UserEntity GetExistAndNotDeletedUserById(int id)
        {
            try
            {
                return _context.Users
                    .Include(u => u.UserRole)
                    .Single(u => u.Id == id && !u.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                //_logger.LogDebug($"{nameof(UserEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $" {(nameof(UserEntity))} with id {id} not found");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }


        // перенести в Сервис
        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id)
        {
            //try
            //{
            //    return _context.SitterWork
            //        .Include(sw => sw.WorkType)
            //        .Include(sw => sw.User)
            //        .Include(sw => sw.LocationWork)
            //        .ThenInclude(lw => lw.TimingLocationWorks.Where(tl => FilterTimingLocationWork(tl, startOrder, endOrder)).ToList())
            //        .Single(sw => sw.User.Id == id);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    // logger.LogDebug($"{nameof(SitterWorkEntity)} with id {id} not found.");
            //    _logger.Log(LogLevel.Debug, $"{nameof(SitterWorkEntity)} with id {id} not found.");
            //    throw new NotFoundException(id, nameof(SitterWorkEntity));
            //}

            return _context.SitterWork
                .Include(sw => sw.WorkType)
                .Include(sw => sw.User)
                .Include(sw => sw.LocationWork)
                .ThenInclude(lw => lw.TimingLocationWorks)
                .ThenInclude(tlw => tlw.DayOfWeek)
                .Where(sw => sw.User.Id == id).ToList();
        }

        public List<OrderEntity> GetOrdersAtWorkOnDateByUserId(int sitterId, DateTime startDate)
        {
            try
            {
                return _context.Orders
                    .Include(o => o.SitterWork)
                    .Where(o => o.OrderStatusId == 4
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

        //private bool FilterTimingLocationWork(TimingLocationWorkEntity timing, DateTime startOrder, DateTime endOrder)
        //{
        //    if ((startOrder.DayOfWeek.ToString() == timing.DayOfWeek.Name
        //           && startOrder.TimeOfDay >= timing.Start)
        //       || (endOrder.DayOfWeek.ToString() == timing.DayOfWeek.Name
        //            && endOrder.TimeOfDay <= timing.Stop))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
