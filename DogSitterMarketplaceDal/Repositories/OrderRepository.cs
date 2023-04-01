
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
            order.Comment = "comment";
            order.Summ = 100;
            order.DateStart = DateTime.Now;
            order.DateEnd = DateTime.Now;
            order.IsDeleted = false;
            order.OrderStatus = new OrderStatusEntity();
            order.SitterWork = new SitterWorkEntity();
            order.Location = new LocationEntity();

            OrderStatusEntity orderStatus = new OrderStatusEntity
            {
                Id = 1,
                Name = "done"
            };
            SitterWorkEntity sitterWork = new SitterWorkEntity
            {
                Id = 10,
                Comment = "10"
            };
            LocationEntity location = new LocationEntity
            {
                Id = 100
            };

            order.OrderStatus.Id = orderStatus.Id;
            order.SitterWork.Id = sitterWork.Id;
            order.Location.Id = location.Id;

            _context.OrderStatuses.Add(orderStatus);
            _context.SitterWork.Add(sitterWork);
            _context.Location.Add(location);
            _context.Orders.Add(order);
            _context.SaveChanges();

            return new OrderEntity
            {
                Id = order.Id,
                Comment = order.Comment,
                Summ = order.Summ,
                DateStart = order.DateStart,
                DateEnd = order.DateEnd,
                IsDeleted = order.IsDeleted,
                OrderStatus = order.OrderStatus,
                SitterWork = order.SitterWork,
                Location = order.Location
            };
        }

        public List<OrderEntity> GetAllOrders()
        {
            return _context.Orders.Where(o => !o.IsDeleted).ToList();
        }

        public OrderEntity GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(OrderEntity order)
        {
            var orderDb = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            orderDb.Summ = order.Summ;

            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var orderDb = _context.Orders.FirstOrDefault(o => o.Id == id);
            orderDb.IsDeleted = false;

            _context.SaveChanges();
        }
    }
}
