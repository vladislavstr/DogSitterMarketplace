﻿using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IOrderService
    {
        public OrderResponse AddOrder(OrderCreateRequest newOrder);

        public List<OrderResponse> GetAllNotDeletedOrders();

        public OrderResponse GetNotDeletedOrderById(int id);

        public void DeleteOrderById(int id);

        public OrderResponse UpdateOrder(OrderUpdate orderUpdate);
    }
}