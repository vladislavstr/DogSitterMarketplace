using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IOrderRepository
    {
        public Task<OrderEntity> AddNewOrder(OrderEntity order);

        public Task<List<OrderEntity>> GetAllOrders();

        public Task<OrderEntity> GetOrderById(int id);

        public Task<OrderEntity> UpdateOrder(OrderEntity orderUpdateEntity);

        public Task DeleteOrderById(int id);

        public Task<OrderStatusEntity> GetOrderStatusById(int id);

        public Task<List<OrderEntity>> GetOrdersAtWorkOnDateByUserId(int sitterId, DateTime startDate);

        public Task<OrderEntity> ChangeOrderStatus(int orderId, int orderStatusId);

        public Task<List<OrderEntity>> GetAllOrdersBySitterId(int userId);

        public Task<OrderStatusEntity> GetOrderStatusByName(string name);

        public Task<List<OrderEntity>> GetOrdersBySitterIdAndClientId(int sitterId, int clientId);
    }
}