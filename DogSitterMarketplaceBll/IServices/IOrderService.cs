﻿using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IOrderService
    {
        public OrderResponse AddOrder(OrderCreateRequest newOrder);

        public List<OrderResponse> GetAllOrders();

        public OrderResponse GetOrderById(int id);

        public void DeleteOrderById(int id);

        public int UpdateOrder(OrderUpdate orderUpdate);
    }
}