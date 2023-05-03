using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Users;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DogSitterMarketplaceApi.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _service;

        private readonly IMapper _mapper;

        //private readonly ILogger logger;

        public AuthenticationController(
            IAuthService authService,
            IMapper autoMapper)//,ILogger nLogger)
        {
            _service = authService;
            _mapper = autoMapper;
            //logger = nLogger;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            //logger.Info("Register request recieved");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var userRegister = mapper.Map<UserRegisterRequest, UserRegister>(request);
            var userRegister = _mapper.Map<UserRegister>(request);
            var authResult = await _service.RegisterUser(userRegister);

            //var response = _mapper.Map<AuthResult, AuthResponse>(authResult);
            var response = _mapper.Map<AuthResponse>(authResult);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var userRegister = _mapper.Map<UserLoginRequest, UserLogin>(request);
            var userRegister = _mapper.Map<UserLogin>(request);
            var loginResult = await _service.LoginEmail(userRegister);

            //var response = _mapper.Map<AuthResult, AuthResponse>(loginResult);
            var response = _mapper.Map<AuthResponse>(loginResult);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
