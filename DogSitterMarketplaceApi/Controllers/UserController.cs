using Microsoft.AspNetCore.Mvc;

using DogSitterMarketplaceDal.Repositories;
using DogSitterMarketplaceApi.Models.UsersDto.Request;

namespace DogSitterMarketplaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository repo;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            repo = new UserRepository();
        }

        [HttpGet]
        public IActionResult GrtPing()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetPoint()
        {
            return Ok(repo.GetUser());
        }

        //[HttpGet("{id}", Name = "GetUserById")]
        //public IActionResult GetUserById(int id)
        //{
        //    return Ok(repo.Get(id));
        //}

        [HttpPost]
        public IActionResult Create(UserRequestDto user)
        {
            var result = repo.CreateUser(user);

            return Ok(result);
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteUserById(int id)
        //{
        //    return StatusCode(StatusCodes.Status204NoContent);
        //}
    }
}
