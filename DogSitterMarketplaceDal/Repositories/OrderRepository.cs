using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DogSitterMarketplaceDal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger<IOrderRepository> _logger;

        public OrderRepository(ILogger<IOrderRepository> logger)
        {
            _context = new OrdersAndPetsAndCommentsContext();
            _logger = logger;
        }

        public OrderEntity AddNewOrder(OrderEntity order)
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
                .Where(o => !o.IsDeleted
                && !o.OrderStatus.IsDeleted
                && !o.SitterWork.IsDeleted
                && !o.SitterWork.User.IsDeleted
                && !o.SitterWork.WorkType.IsDeleted
                ).ToList();
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
                    .Single(o => o.Id == id && !o.IsDeleted
                     && !o.OrderStatus.IsDeleted
                     && !o.SitterWork.IsDeleted
                     && !o.SitterWork.User.IsDeleted
                     && !o.SitterWork.WorkType.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug($"{nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public int UpdateOrder(OrderEntity orderUpdateEntity)
        {
            // _context.Orders.Update(orderUpdateEntity);

            var orderDB = _context.Orders
                .Include(o => o.Pets)
                .SingleOrDefault(o => o.Id == orderUpdateEntity.Id && !o.IsDeleted);

            if (orderDB == null)
            {
                _logger.LogDebug($"{nameof(OrderEntity)} with id {orderUpdateEntity.Id} not found.");
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

            return orderDB.Id;
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
                _logger.LogDebug($"{nameof(OrderEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderEntity));
            }
        }

        public LocationEntity GetLocationById(int id)
        {
            try
            {
                return _context.Location.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug($"{nameof(LocationEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(LocationEntity));
            }
        }

        public OrderStatusEntity GetOrderStatusById(int id)
        {
            try
            {
                return _context.OrderStatuses.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug($"{nameof(OrderStatusEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(OrderStatusEntity));
            }
        }

        public SitterWorkEntity GetSitterWorkById(int id)
        {
            try
            {
                return _context.SitterWork.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug($"{nameof(SitterWorkEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(SitterWorkEntity));
            }
        }

        public List<PetEntity> GetPetsInOrderEntities(List<int> pets)
        {
            if (!pets.Any())
            {
                return new List<PetEntity>();
            }

            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .Where(p => !p.IsDeleted && pets.Contains(p.Id)
                && !p.Type.IsDeleted
                && !p.User.IsDeleted)
                .ToList();
        }
    }
}
