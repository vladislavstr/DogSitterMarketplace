using AutoMapper;
using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper, ILogger logger)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public ActionResult<List<UserResponseDto>> GetAllUsers()
        {
            try
            {
                var allUsers = _userService.GetAllUsers();
                var allUsersDto = _mapper.Map<List<UserResponseDto>>(allUsers);
                return Ok(allUsersDto);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(GetAllUsers)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllNotDeletedUsers", Name = "GetAllNotDeletedUsers")]
        public ActionResult GetAllNotDeletedUsers()
        {
            try
            {
                return Ok(_userService.GetAllNotDeletedUsers());
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(GetAllNotDeletedUsers)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult GetUserById(int id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(GetUserById)}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}", Name = "DeleteUserById")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                _userService.DeleteUserById(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(DeleteUserById)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddUser", Name = "AddUser")]
        public ActionResult<UserResponseDto> AddUser(UserRequestDto user)
        {
            try
            {
                var userRequst = _mapper.Map<UserRequest>(user);
                var addUserResponse = _userService.AddUser(userRequst);
                var addUserResponseDto = _mapper.Map<UserResponseDto>(addUserResponse);

                return Created(new Uri("api/User", UriKind.Relative), addUserResponseDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(AddUser)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PassportData", Name = "AddUserPassportData")]
        public ActionResult<UserPassportDataResponseDto> AddUserPassportData(UserPassportDataRequestDto userPassportData)
        {
            try
            {
                var userPassportDataRequst = _mapper.Map<UserPassportDataRequest>(userPassportData);
                var addUserPassportDataResponse = _userService.AddUserPassportData(userPassportDataRequst);
                var addUserPassportDataResponseDto = _mapper.Map<UserPassportDataResponseDto>(addUserPassportDataResponse);

                return Created(new Uri("api/User", UriKind.Relative), addUserPassportDataResponseDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(AddUserPassportData)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Status", Name = "AddUserStatus")]
        public ActionResult<UserStatusResponseDto> AddUserStatus(UserStatusRequestDto userStatus)
        {
            try
            {
                var userStatusRequst = _mapper.Map<UserStatusRequest>(userStatus);
                var addUserStatusResponse = _userService.AddUserStatus(userStatusRequst);
                var addUserStatusResponseDto = _mapper.Map<UserStatusResponseDto>(addUserStatusResponse);

                return Created(new Uri("api/User", UriKind.Relative), addUserStatusResponseDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
  
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(AddUserStatus)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("allSittersByLocation/{locationId}", Name = "GetAllSittersByLocationId")]
        public async Task<ActionResult<List<UserShortLocationWorkResponseDto>>> GetAllSittersByLocationId(int locationId)
        {
            try
            {
                var allSittersResponse = await _userService.GetAllSittersByLocationId(locationId);
                var allSittersResponseDto = _mapper.Map<List<UserShortLocationWorkResponseDto>>(allSittersResponse);

                return Ok(allSittersResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"{nameof(OrderController)} {nameof(GetAllNotDeletedOrders)}");
                //_logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(UserController)} {nameof(GetAllSittersByLocationId)}");
                return BadRequest();
            }
        }
    }
}
