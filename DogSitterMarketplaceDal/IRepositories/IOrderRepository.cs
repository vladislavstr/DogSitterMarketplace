using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IOrderRepository
    {
        public OrderEntity AddNewOrder(OrderEntity order);

        public List<OrderEntity> GetAllOrders();

        public OrderEntity GetOrderById(int id);

        public void UpdateOrder(OrderEntity order);

        public void DeleteOrderById(int id);

    }
}
