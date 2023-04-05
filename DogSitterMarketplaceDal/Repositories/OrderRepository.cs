
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

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
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public List<OrderEntity> GetAllOrders()
        {
            return _context.Orders.Where(o => !o.IsDeleted).ToList();
        }

        public OrderEntity GetOrderById(int id)
        {
            return _context.Orders.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
        }

        public void UpdateOrder(OrderEntity order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrderById(int id)
        {
            var orderDb = _context.Orders.SingleOrDefault(o => o.Id == id);
            orderDb.IsDeleted = true;

            _context.SaveChanges();
        }

        public LocationEntity GetLocationById(int id)
        {
            return _context.Location.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
        }

        public OrderStatusEntity GetOrderStatusById(int id)
        {
            return _context.OrderStatuses.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
        }

        public SitterWorkEntity GetSitterWorkById(int id)
        {
            return _context.SitterWork.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
        }

        public List<PetEntity> GetPetsInOrderEntities(List<int> pets)
        {
            if (!pets.Any())
            {
                return new List<PetEntity>();
            }

            return _context.Pets.Where(p => !p.IsDeleted && pets.Contains(p.Id)).ToList();
        }
    }
}
