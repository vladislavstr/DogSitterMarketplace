using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IOrderRepository
    {
        public OrderEntity AddNewOrder(OrderEntity order);

        public List<OrderEntity> GetAllOrders();

        public OrderEntity GetOrderById(int id);

        public OrderEntity UpdateOrder(OrderEntity order);

        public void DeleteOrderById(int id);

        public LocationEntity GetLocationById(int id);

        public OrderStatusEntity GetOrderStatusById(int id);

        public SitterWorkEntity GetSitterWorkById(int id);

        //public List<PetEntity> GetPetsInOrderEntities(List<int> pets);

        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id);

        public List<OrderEntity> GetOrdersAtWorkOnDateByUserId(int sitterId, DateTime startDate);

        public UserEntity GetUserById(int id);

        public OrderEntity ChangeOrderStatusToAtWork(int orderId);
    }
}