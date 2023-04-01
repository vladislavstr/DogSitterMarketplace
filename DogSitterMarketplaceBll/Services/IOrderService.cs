using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceBll.Services
{
    public interface IOrderService
    {
        OrderResponse AddOrder(OrderRequest newOrder);
        List<OrderResponse> GetAllOrders();
        OrderResponse GetOrderById(int id);
    }
}