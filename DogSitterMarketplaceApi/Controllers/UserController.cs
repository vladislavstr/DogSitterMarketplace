using Microsoft.AspNetCore.Mvc;
using AutoMapper;

//using NLog.Web;
//using ILogger = NLog.ILogger;

using DogSitterMarketplaceDal.Repositories;
using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.Models.Users.Request;

namespace DogSitterMarketplaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)//, ILogger logger)
        {
            //_logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GrtPing()
        {
            return Ok();
        }

        //[HttpGet("{id}", Name = "GetUserById")]
        //public IActionResult GetPoint()
        //{
        //    return Ok(_userService.GetAllUsers());
        //}

        //////[HttpGet("{id}", Name = "GetUserById")]
        //////public IActionResult GetUserById(int id)
        //////{
        //////    return Ok(repo.Get(id));
        //////}

        ////[HttpPost]
        ////public IActionResult Create(UserRequestDto user)
        ////{
        ////    var result = repo.CreateUser(user);

        ////    return Ok(result);
        ////}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteUserById(int id)
        //{
        //    return StatusCode(StatusCodes.Status204NoContent);
        //}

        //[HttpPost(Name = "AddUser")]
        //public ActionResult<UserResponseDto> AddUser(UserRequestDto user)
        //{
        //    try
        //    {
        //        var userRequst = _mapper.Map<UsersRequest>(user);
        //        var addUserResponse = _userService.AddUser(userRequst);
        //        var addUserResponseDto = _mapper.Map<PetResponseDto>(addUserResponse);

        //        return Created(new Uri("api/Pet", UriKind.Relative), addUserResponseDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
