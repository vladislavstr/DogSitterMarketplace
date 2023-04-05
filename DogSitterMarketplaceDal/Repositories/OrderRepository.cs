
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static DogSitterContext _context;

        public OrderRepository()
        {
            _context = new DogSitterContext();
        }

        public OrderEntity AddNewOrder(OrderEntity order)
        {
            //order.Comment = "comment";
            //order.Summ = 100;
            //order.DateStart = DateTime.Now;
            //order.DateEnd = DateTime.Now;
            //order.IsDeleted = false;
            //order.OrderStatus = new OrderStatusEntity();
            //order.SitterWork = new SitterWorkEntity();
            //order.Location = new LocationEntity();

            //OrderStatusEntity orderStatus = new OrderStatusEntity
            //{
            //    Id = 1,
            //    Name = "done"
            //};
            //SitterWorkEntity sitterWork = new SitterWorkEntity
            //{
            //    Id = 10,
            //    Comment = "10"
            //};
            //LocationEntity location = new LocationEntity
            //{
            //    Id = 100
            //};

            //order.OrderStatus.Id = orderStatus.Id;
            //order.SitterWork.Id = sitterWork.Id;
            //order.Location.Id = location.Id;

            //_context.OrderStatuses.Add(order.OrderStatus);
            //_context.SitterWork.Add(order.SitterWork);
            //_context.Location.Add(order.Location);

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;

            //return new OrderEntity
            //{
            //    Id = order.Id,
            //    Comment = order.Comment,
            //    Summ = order.Summ,
            //    DateStart = order.DateStart,
            //    DateEnd = order.DateEnd,
            //    IsDeleted = order.IsDeleted,
            //    OrderStatus = order.OrderStatus,
            //    SitterWork = order.SitterWork,
            //    Location = order.Location
            //};
        }

        public List<OrderEntity> GetAllOrders()
        {
            return _context.Orders.Where(o => !o.IsDeleted).ToList();
        }

        public OrderEntity GetOrderById(int id)
        {
            return _context.Orders.Single(o => o.Id == id);
        }

        public void UpdateOrder(OrderEntity order)
        {
            var orderDb = _context.Orders.Single(o => o.Id == order.Id);
            //orderDb.Comment = order.Comment;
            //orderDb.OrderStatus = order.OrderStatus;
            //orderDb.SitterWork = order.SitterWork;
            //orderDb.Summ = order.Summ;
            //orderDb.DateStart = order.DateStart;
            //orderDb.Data = order.DateStart;
            //orderDb.DateStart = order.DateStart;
            //orderDb.DateStart = order.DateStart;

            orderDb = order;

            _context.SaveChanges();
        }

        public void DeleteOrderById(int id)
        {
            var orderDb = _context.Orders.Single(o => o.Id == id);
            orderDb.IsDeleted = false;

            _context.SaveChanges();
        }
    }
}
