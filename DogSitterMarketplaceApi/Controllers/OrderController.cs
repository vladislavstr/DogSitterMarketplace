using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger nLogger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = nLogger;
        }

        [HttpPost(Name = "AddOrder")]
        [SwaggerOperation(Summary = "Add Order")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<OrderResponseDto>> AddOrder(OrderCreateRequestDto addOrder)
        {
            try
            {
                var orderRequest = _mapper.Map<OrderCreateRequest>(addOrder);
                var addOrderResponse = await _orderService.AddOrder(orderRequest);
                var addOrderResponseDto = _mapper.Map<OrderResponseDto>(addOrderResponse);

                return Created(new Uri("api/Order", UriKind.Relative), addOrderResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(AddOrder)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addSeveralOrders", Name = "AddSeveralOrdersForOneClientFromOneSitter")]
        [SwaggerOperation(Summary = "Add Several Orders For One Client From One Sitter")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<List<OrderResponseDto>>> AddSeveralOrdersForOneClientFromOneSitter(List<OrderCreateRequestDto> addOrders)
        {
            try
            {
                var ordersRequest = _mapper.Map<List<OrderCreateRequest>>(addOrders);
                var addOrdersResponse = await _orderService.AddSeveralOrdersForOneClientFromOneSitter(ordersRequest);
                var addOrdersResponseDto = _mapper.Map<List<OrderResponseDto>>(addOrdersResponse);

                return Created(new Uri("api/Order", UriKind.Relative), addOrdersResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(AddSeveralOrdersForOneClientFromOneSitter)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}", Name = "ChangeOrderStatus")]
        [SwaggerOperation(Summary = "Change Order Status")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<OrderResponseDto>> ChangeOrderStatus(int id, int orderStatusId)
        {
            try
            {
                var updateOrderResponse = await _orderService.ChangeOrderStatus(id, orderStatusId);
                var updateOrderResponseDto = _mapper.Map<OrderResponseDto>(updateOrderResponse);

                return Ok(updateOrderResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(ChangeOrderStatus)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/ordersUnderConsideration", Name = "GetAllOrdersUnderConsiderationBySitterId")]
        [SwaggerOperation(Summary = "Get All Orders Under Consideration By Sitter Id")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<List<OrderResponseDto>>> GetAllOrdersUnderConsiderationBySitterId(int userId)
        {
            try
            {
                var ordersResponse = await _orderService.GetAllOrdersUnderConsiderationBySitterId(userId);
                var ordersResponseDto = _mapper.Map<List<OrderResponseDto>>(ordersResponse);

                return Ok(ordersResponseDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(GetAllOrdersUnderConsiderationBySitterId)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "GetAllNotDeletedOrders")]
        [SwaggerOperation(Summary = "Get All Not Deleted Orders")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<OrderResponseDto>>> GetAllNotDeletedOrders()
        {
            try
            {
                var ordersResponse = await _orderService.GetAllNotDeletedOrders();
                var ordersResponseDto = _mapper.Map<List<OrderResponseDto>>(ordersResponse);

                return Ok(ordersResponseDto);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(GetAllNotDeletedOrders)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetNotDeletedOrderById")]
        [SwaggerOperation(Summary = "Get Not Deleted Order By Id")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<OrderResponseDto>> GetNotDeletedOrderById(int id)
        {
            try
            {
                var orderResponse = await _orderService.GetNotDeletedOrderById(id);
                var orderResponseDto = _mapper.Map<OrderResponseDto>(orderResponse);

                return Ok(orderResponseDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(GetNotDeletedOrderById)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteOrderById")]
        [SwaggerOperation(Summary = "Delete Order By Id")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            try
            {
                await _orderService.DeleteOrderById(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(DeleteOrderById)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateOrder")]
        [SwaggerOperation(Summary = "Update Order")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<OrderResponseDto>> UpdateOrder(OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var orderUpdate = _mapper.Map<OrderUpdate>(orderUpdateDto);
                var orderResponse = await _orderService.UpdateOrder(orderUpdate);
                var orderResponseDto = _mapper.Map<OrderResponseDto>(orderResponse);

                return Ok(orderResponseDto);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(OrderController)} {nameof(UpdateOrder)}");
                return BadRequest(ex.Message);
            }
        }
    }
}