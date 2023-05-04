﻿using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceBll.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger nLogger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = nLogger;
        }

        public List<UserResponse> GetAllUsers()
        {
            _logger.Log(LogLevel.Info, $"{nameof(UserService)} start {nameof(GetAllUsers)}");

            var allusersEntitys = _userRepository.GetAllUsers();
            var userResponse = _mapper.Map<List<UserResponse>>(allusersEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(UserService)} end {nameof(GetAllUsers)}");

            return userResponse;
        }

        public ICollection<UserResponse> GetAllNotDeletedUsers()
        {
            _logger.Log(LogLevel.Info, $"{nameof(UserService)} start {nameof(GetAllNotDeletedUsers)}");

            var allusersEntitys = _userRepository.GetAllUsers();
            var usersEntitys = allusersEntitys
                           .Where(u => !u.IsDeleted);
            var userResponse = _mapper.Map<ICollection<UserResponse>>(usersEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(UserService)} end {nameof(GetAllNotDeletedUsers)}");

            return userResponse;
        }

        public UserResponse GetUserById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(UserService)} start {nameof(GetUserById)}");

            var usersEntitys = _userRepository.GetUserById(id);
            var userResponse = _mapper.Map<UserResponse>(usersEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(UserService)} end {nameof(GetUserById)}");

            return userResponse;
        }

        public UserResponse AddUser(UserRequest user)
        {
            _logger.Log(LogLevel.Info, $"{nameof(UserService)} start {nameof(AddUser)}");

            List<UserResponse> usersResponse = new List<UserResponse>();
            usersResponse = GetAllNotDeletedUsers().ToList();
            if (usersResponse.Exists(u => u.Email == user.Email && u.UserStatus.Id == 2))
            {
                throw new ArgumentException($"User with Email {user.Email} is banned");
            }
            else if (usersResponse.Exists(u => u.Email == user.Email)) 
            {
                throw new ArgumentException($"User with Email {user.Email} is exist");
            }
            else 
            { 
            var userEntity = _mapper.Map<UserEntity>(user);
            var addUserEntity = _userRepository.AddUser(userEntity);
            var addUserResponse = _mapper.Map<UserResponse>(addUserEntity);

            _logger.Log(LogLevel.Info, $"{nameof(UserService)} end {nameof(AddUser)}");

            return addUserResponse;
            }
        }

        public UserPassportDataResponse AddUserPassportData(UserPassportDataRequest PassportData)
        {
            _logger.Log(LogLevel.Info, $"{nameof(UserService)} start {nameof(AddUserPassportData)}");

            var userPassportDataEntity = _mapper.Map<UserPassportDataEntity>(PassportData);
            var addUserPassportDataEntity = _userRepository.AddUserPassportData(userPassportDataEntity);
            var addUserPassportDataResponse = _mapper.Map<UserPassportDataResponse>(addUserPassportDataEntity);

            _logger.Log(LogLevel.Info, $"{nameof(UserService)} end {nameof(AddUserPassportData)}");

            return addUserPassportDataResponse;
        }

        public UserStatusResponse AddUserStatus(UserStatusRequest userStatus)
        {
            _logger.Log(LogLevel.Info, $"{nameof(UserService)} start {nameof(AddUserStatus)}");

            var userStatusEntity = _mapper.Map<UserStatusEntity>(userStatus);
            var adduserStatusEntity = _userRepository.AddUserStatus(userStatusEntity);
            var adduserStatusResponse = _mapper.Map<UserStatusResponse>(adduserStatusEntity);

            _logger.Log(LogLevel.Info, $"{nameof(UserService)} end {nameof(AddUserStatus)}");

            return adduserStatusResponse;
        }

        //public UserResponse AddUser(UserRequest user)
        //{
        //    var userEntity = _mapper.Map<UserEntity, UserEntity>(user);
        //    var addUserEntity = _userRepository.AddUser(userEntity);
        //    var addUserResponse = _mapper.Map<UserResponse>(addUserEntity);

        //    return addUserResponse;
        //}
        //public Order Create(Order order)
        //{
        //    var orderDal = mapper.Map<Order, OrderDal>(order);
        //    var resultDal = repository.Create(orderDal);

        //    return mapper.Map<OrderDal, Order>(resultDal);
        //}

        public void DeleteUserById(int id)
        {
            _userRepository.DeleteUserById(id);
        }

        public void BlockingUserById(int id)
        {
            _userRepository.BlockingUserById(id);
        }

        //добавить логгер
        public async Task<List<UserShortLocationWorkResponse>> GetAllSittersByLocationId(int locationId)
        {
            //var userEntity = _userRepository.GetUserWithRoleById(clientId);
            //var userRole = userEntity.UserRole;

            //if (userRole.Name == UserRole.Client)
            //{
            var allSittersEntity = await _userRepository.GetAllSittersByLocationId(locationId);
            var allSittersIsActiveEntity = allSittersEntity.Where(s => s.SitterWorks.Any(sw => sw.LocationWork.Any(lw => !lw.IsNotActive && lw.LocationId == locationId))).ToList();
            //    var usersShortsResponse = _mapper.Map<List<UserShortLocationWorkResponse>>(allSittersIsActiveEntity);
            var usersShortsResponse = allSittersIsActiveEntity.Select(s => new UserShortLocationWorkResponse
            {
                Id = s.Id,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Name = s.Name,
                WorkTypesPrices = s.SitterWorks.Where(sw => sw.LocationWork.Any(lw => !lw.IsNotActive && lw.LocationId == locationId))
                .Select(sw => new WorkTypePriceResponse
                {
                    Price = sw.LocationWork.First(l => l.LocationId == locationId).Price,
                    WorkType = _mapper.Map<WorkTypeResponse>(sw.WorkType)
                }).ToList()
            }).ToList();

            return usersShortsResponse;
            //}
            //else
            //{
            //    // _logger.Log(LogLevel.Debug, $"{nameof(UserService)} {nameof(GetAllSittersByLocationId)} User with id {clientId} does not have nessety role for get List os Sitters");
            //    throw new ArgumentException($"User with id {clientId} does not have nessety role for get List os Sitters");
            //}
        }
    }
}
