﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.IServices;

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

        [HttpGet("GrtPing")]
        public IActionResult GrtPing()
        {
            return Ok();
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
                return Ok(ex.Message);
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
                return Ok(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult GetUserById(int id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
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
            catch (Exception ex)
            {
                return Ok(ex.Message);
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
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}