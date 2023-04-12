using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using DogSitterMarketplaceDal.Repositories;
using System.Linq;

namespace DogSitterMarketplaceBll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderReposotory;

        private readonly IPetRepository _petReposotory;

        public OrderService(IOrderRepository orderReposotory, IPetRepository petReposotory, IMapper mapper)
        {
            _orderReposotory = orderReposotory;
            _petReposotory = petReposotory;
            _mapper = mapper;
        }

        public OrderResponse AddOrder(OrderCreateRequest newOrder)
        {
            var orderEntity = _mapper.Map<OrderEntity>(newOrder);
            orderEntity.Pets.AddRange(_petReposotory.GetPetsInOrderEntities(newOrder.Pets));
            var addOrderEntity = _orderReposotory.AddNewOrder(orderEntity);
            var addOrderResponse = _mapper.Map<OrderResponse>(addOrderEntity);

            return addOrderResponse;
        }

        public List<OrderResponse> GetAllNotDeletedOrders()
        {
            var allOrdersEntity = _orderReposotory.GetAllOrders();
            var ordersEntity = allOrdersEntity
                .Where(o => !o.IsDeleted)
                .Select(o => new OrderEntity
                {
                    Id = o.Id,
                    Comment = o.Comment,
                    OrderStatus = o.OrderStatus,
                    OrderStatusId = o.OrderStatusId,
                    SitterWork = o.SitterWork,
                    SitterWorkId = o.SitterWorkId,
                    Summ = o.Summ,
                    DateStart = o.DateStart,
                    DateEnd = o.DateEnd,
                    Location = o.Location,
                    LocationId = o.LocationId,
                    IsDeleted = o.IsDeleted,
                    Comments = o.Comments.Where(c => !c.IsDeleted).ToList(),
                    Appeals = o.Appeals.Where(a => !a.IsDeleted).ToList(),
                    //  Pets = o.Pets
                });
            var ordersResponse = _mapper.Map<List<OrderResponse>>(ordersEntity);

            return ordersResponse;
        }

        public OrderResponse GetNotDeletedOrderById(int id)
        {
            var orderEntity = _orderReposotory.GetOrderById(id);

            if (!orderEntity.IsDeleted)
            {
                orderEntity.Comments = orderEntity.Comments.Where(c => !c.IsDeleted).ToList();
                orderEntity.Appeals = orderEntity.Appeals.Where(c => !c.IsDeleted).ToList();
                var orderResponse = _mapper.Map<OrderResponse>(orderEntity);

                return orderResponse;
            }
            else
            {
                // что возвращать?  + logger??
                throw new NotFoundException(id, nameof(orderEntity));
            }
        }

        public void DeleteOrderById(int id)
        {
            _orderReposotory.DeleteOrderById(id);
        }

        public OrderResponse UpdateOrder(OrderUpdate orderUpdate)
        {
            var orderEntity = _mapper.Map<OrderEntity>(orderUpdate);
            orderEntity.OrderStatus = _orderReposotory.GetOrderStatusById(orderUpdate.OrderStatusId);

            //перенести методы-проверки в Сервис
            orderEntity.SitterWork = _orderReposotory.GetSitterWorkById(orderUpdate.SitterWorkId);
            orderEntity.Location = _orderReposotory.GetLocationById(orderUpdate.LocationId);

            // orderEntity.Pets.AddRange(_petReposotory.GetPetsInOrderEntities(orderUpdate.Pets));

            var allPets = _petReposotory.GetPetsInOrderEntities(orderUpdate.Pets);
            List<PetEntity> notMatchPets = new List<PetEntity>();
            if (allPets.Count < orderUpdate.Pets.Count)
            {
                foreach (int petId in orderUpdate.Pets)
                {
                    var match = allPets.FirstOrDefault(p => p.Id == petId);
                    if (match != null)
                    {
                        notMatchPets.Add(match);
                    }
                }
            }

            if (orderEntity.Pets.Any(p => p.IsDeleted))
            { 
            // сщщбщение куда записывать??
            }

            orderEntity.Pets.AddRange(allPets.Where(p => !p.IsDeleted));
            var updateOrderEntity = _orderReposotory.UpdateOrder(orderEntity);
            var updateOrderResponse = _mapper.Map<OrderResponse>(updateOrderEntity);

            if (notMatchPets.Any())
            {
                foreach (var matchPet in notMatchPets)
                {
                    updateOrderResponse.Messages.Add($"Pet with id {matchPet.Id} not found now");
                }
            }

            return updateOrderResponse;
        }
    }
}