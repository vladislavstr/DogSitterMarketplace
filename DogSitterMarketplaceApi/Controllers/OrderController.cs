using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using Microsoft.AspNetCore.Mvc;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost(Name = "AddOrder")]
        public OrderResponseDto AddOrder(OrderRequestDto addOrder)
        {
            try
            {
                var orderRequest = _mapper.Map<OrderRequest>(addOrder);
                var addOrderResponse = _orderService.AddOrder(orderRequest);
                var addOrderResponseDto = _mapper.Map<OrderResponseDto>(addOrderResponse);

                return addOrderResponseDto;

                //var orderRequest = new OrderRequest
                //{
                //    LocationId = addOrder.LocationId,
                //    Summ = addOrder.Summ
                //};

                //var addOrderRequest = _orderService.AddOrder(orderRequest);

                //var orderResponseDto = new OrderResponseDto
                //{
                //    Id = addOrderRequest.Id,
                //    Summ = addOrderRequest.Summ
                //};

                //if (addOrderRequest.Location != null)
                //{
                //    orderResponseDto.Location = new LocationResponseDto
                //    {
                //        Id = addOrderRequest.Location.Id
                //    };
                //}
                //return orderResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(OrderController)} {nameof(AddOrder)}");
                return null;
            }
        }


        [HttpGet(Name = "GetAllOrders")]
        public IEnumerable<OrderResponseDto> GetAllOrders()
        {
            try
            {
                var ordersResponse = _orderService.GetAllOrders();
                var ordersResponseDto = _mapper.Map<List<OrderResponseDto>>(ordersResponse);

                return ordersResponseDto;

                //var ordersbll = _orderService.GetAllOrders();
                //return ordersbll.Select(b =>
                //{
                //    var orderResponseDto = new OrderResponseDto
                //    {
                //        Id = b.Id,
                //        Comment = b.Comment
                //    };

                //    if (b.OrderStatus != null)
                //    {
                //        orderResponseDto.OrderStatus = new OrderStatusResponseDto
                //        {
                //            Id = b.OrderStatus.Id,
                //            Comment = b.OrderStatus.Comment,
                //        };
                //    };

                //    if (b.Pets != null)
                //    {
                //        orderResponseDto.Pets = b.Pets.Select(r => new PetResponseDto
                //        {
                //            Id = r.Id,
                //            Name = r.Name,
                //        }).ToList();
                //    }

                //    return orderResponseDto;
                //}).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(OrderController)} {nameof(GetAllOrders)}");
                return Enumerable.Empty<OrderResponseDto>();
            }
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public OrderResponseDto GetOrderById(int id)
        {
            try
            {
                //var orderResponse = _orderService.GetOrderById(id);

                //return new OrderResponseDto
                //{
                //    Id = orderResponse.Id
                //};

                var orderResponse = _orderService.GetOrderById(id);
                var orderResponseDto = _mapper.Map<OrderResponseDto>(orderResponse);

                return orderResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(OrderController)} {nameof(GetOrderById)}");
                return null;
            }
        }

        [HttpDelete(Name = "DeleteOrderById")]
        public IActionResult DeleteOrderById(int id)
        {
            try
            {
                return NoContent(_orderService.DeleteOrderById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(OrderController)} {nameof(DeleteOrderById)}");
                return null;
            }
        }
    }
}