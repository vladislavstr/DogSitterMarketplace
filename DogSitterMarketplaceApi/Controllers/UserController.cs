using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;

using DogSitterMarketplaceDal.Repository;

namespace DogSitterMarketplaceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository repo;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            repo = new UserRepository();
        }

        public IActionResult GetPoint()
        {
            return Ok(repo.Get());
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById(int id)
        {
            return Ok(repo.Get(id));
        }

        [HttpPost]
        public IActionResult CreateForcast(UserEntity user)
        {
            var result = repo.Create(user);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
