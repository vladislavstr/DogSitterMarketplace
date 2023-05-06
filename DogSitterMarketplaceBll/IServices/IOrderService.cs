using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IOrderService
    {
        public Task<OrderResponse> AddOrder(OrderCreateRequest newOrder);

        public Task<List<OrderResponse>> GetAllNotDeletedOrders();

        public Task<OrderResponse> GetNotDeletedOrderById(int id);

        public Task DeleteOrderById(int id);

        public Task<OrderResponse> UpdateOrder(OrderUpdate orderUpdate);

        public Task<OrderResponse> ChangeOrderStatus(int orderId, int orderStatusId);

        public Task<OrderResponse> GetOrderOfTrow(int orderId);

        public Task<List<OrderResponse>> GetAllOrdersUnderConsiderationBySitterId(int userId);

        public Task<List<OrderResponse>> AddSeveralOrdersForOneClientFromOneSitter(List<OrderCreateRequest> orders);
    }
}