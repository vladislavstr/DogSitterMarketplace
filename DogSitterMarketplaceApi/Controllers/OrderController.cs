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
using NLog;
using ILogger = NLog.ILogger;
using System.Threading.Tasks;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        // private readonly ILogger<OrderController> _logger;

        private readonly ILogger _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger nLogger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = nLogger;
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
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}", Name = "ChangeOrderStatus")]
        public ActionResult<OrderResponseDto> ChangeOrderStatus(int id, int orderStatusId)
        {
            try
            {
                var updateOrderResponse = _orderService.ChangeOrderStatus(id, orderStatusId);
                var updateOrderResponseDto = _mapper.Map<OrderResponseDto>(updateOrderResponse);

                return Ok(updateOrderResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet(Name = "GetAllNotDeletedOrders")]
        public ActionResult<List<OrderResponseDto>> GetAllNotDeletedOrders()
        {
            try
            {
                var ordersResponse = _orderService.GetAllNotDeletedOrders();
                var ordersResponseDto = _mapper.Map<List<OrderResponseDto>>(ordersResponse);

                return Ok(ordersResponseDto);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"{nameof(OrderController)} {nameof(GetAllNotDeletedOrders)}");
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(GetAllNotDeletedOrders)}");
                return Problem();
            }
        }

        [HttpGet("{id}", Name = "GetNotDeletedOrderById")]
        public ActionResult<OrderResponseDto> GetNotDeletedOrderById(int id)
        {
            try
            {
                var orderResponse = _orderService.GetNotDeletedOrderById(id);
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
        public ActionResult<OrderResponseDto> UpdateOrder(OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var orderUpdate = _mapper.Map<OrderUpdate>(orderUpdateDto);
                var orderResponse = _orderService.UpdateOrder(orderUpdate);
                var orderResponseDto = _mapper.Map<OrderResponseDto>(orderResponse);

                return Ok(orderResponseDto);

            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}