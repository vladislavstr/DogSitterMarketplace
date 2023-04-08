using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        public OrderRepository()
        {
            _context = new OrdersAndPetsAndCommentsContext();
        }

        public OrderEntity AddNewOrder(OrderEntity order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.SitterWork)
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
                .Include(o => o.Comments)
                .Include(o => o.Appeals)
                .Include(o => o.Pets)
                .Where(o => !o.IsDeleted).ToList();
        }

        public OrderEntity GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.SitterWork)
                .Include(o => o.Location)
                .Include(o => o.SitterWork.User)
                .Include(o => o.Comments)
                .Include(o => o.Appeals)
                .Include(o => o.Pets)
                .Single(o => o.Id == id && !o.IsDeleted);
        }

        public int UpdateOrder(OrderEntity orderUpdateEntity)
        {
            // _context.Orders.Update(orderUpdateEntity);

            var orderDB = _context.Orders
                .Include(o => o.Pets)
                .Single(o => o.Id == orderUpdateEntity.Id);
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
            var orderDb = _context.Orders.Single(o => o.Id == id && !o.IsDeleted);
            orderDb.IsDeleted = true;

            _context.SaveChanges();
        }

        public LocationEntity GetLocationById(int id)
        {
            return _context.Location.Single(o => o.Id == id && !o.IsDeleted);
        }

        public OrderStatusEntity GetOrderStatusById(int id)
        {
            return _context.OrderStatuses.Single(o => o.Id == id && !o.IsDeleted);
        }

        public SitterWorkEntity GetSitterWorkById(int id)
        {
            return _context.SitterWork.Single(o => o.Id == id && !o.IsDeleted);
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
                .Where(p => !p.IsDeleted && pets.Contains(p.Id)).ToList();
        }

        //public List<CommentEntity> GetCommentsById(List<int> comments)
        //{
        //    if (!comments.Any())
        //    {
        //        return new List<CommentEntity>();
        //    }

        //    return _context.Comments.Where(c => !c.IsDeleted && comments.Contains(c.Id)).ToList();
        //}

        //public List<AppealEntity> GetAppealsById(List<int> appeals)
        //{
        //    if (!appeals.Any())
        //    {
        //        return new List<AppealEntity>();
        //    }

        //    return _context.Appeals.Where(a => !a.IsDeleted && appeals.Contains(a.Id)).ToList();
        //}
    }
}
