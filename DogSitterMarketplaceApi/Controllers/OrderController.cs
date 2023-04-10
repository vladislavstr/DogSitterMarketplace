using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

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
        public ActionResult<OrderResponseDto> AddOrder(OrderCreateRequestDto addOrder)
        {
            try
            {
                var orderRequest = _mapper.Map<OrderCreateRequest>(addOrder);
                var addOrderResponse = _orderService.AddOrder(orderRequest);
                var addOrderResponseDto = _mapper.Map<OrderResponseDto>(addOrderResponse);

                return Created(new Uri("api/Order", UriKind.Relative), addOrderResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(OrderController)} {nameof(AddOrder)}");
                return Problem();
            }
        }

        [HttpGet(Name = "GetAllOrders")]
        public ActionResult<List<OrderResponseDto>> GetAllOrders()
        {
            try
            {
                var ordersResponse = _orderService.GetAllOrders();
                var ordersResponseDto = _mapper.Map<List<OrderResponseDto>>(ordersResponse);

                return Ok(ordersResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(OrderController)} {nameof(GetAllOrders)}");
                return Problem();
            }
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public ActionResult<OrderResponseDto> GetOrderById(int id)
        {
            try
            {
                var orderResponse = _orderService.GetOrderById(id);
                var orderResponseDto = _mapper.Map<OrderResponseDto>(orderResponse);

                return Ok(orderResponseDto);
            }

            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}", Name = "DeleteOrderById")]
        public IActionResult DeleteOrderById(int id)
        {
            try
            {
                _orderService.DeleteOrderById(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}", Name = "UpdateOrder")]
        public ActionResult<int> UpdateOrder(OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var orderUpdate = _mapper.Map<OrderUpdate>(orderUpdateDto);
                int id = _orderService.UpdateOrder(orderUpdate);

                return Ok(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}