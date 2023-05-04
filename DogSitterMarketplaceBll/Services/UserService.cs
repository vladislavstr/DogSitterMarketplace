using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceBll.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<UserResponse> GetAllUsers()
        {
            var allusersEntitys = _userRepository.GetAllUsers();
            var userResponse = _mapper.Map<List<UserResponse>>(allusersEntitys);

            return userResponse;
        }

        public ICollection<UserResponse> GetAllNotDeletedUsers()
        {
            var allusersEntitys = _userRepository.GetAllUsers();
            var usersEntitys = allusersEntitys
                           .Where(u => !u.IsDeleted);
            var userResponse = _mapper.Map<ICollection<UserResponse>>(usersEntitys);

            return userResponse;
        }

        public UserResponse GetUserById(int id)
        {
            var usersEntitys = _userRepository.GetUserById(id);
            var userResponse = _mapper.Map<UserResponse>(usersEntitys);

            return userResponse;
        }

        public UserResponse AddUser(UserRequest user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            var addUserEntity = _userRepository.AddUser(userEntity);
            var addUserResponse = _mapper.Map<UserResponse>(addUserEntity);

            return addUserResponse;
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

        //добавить логгер
        public List<UserShortResponse> GetAllSittersForClientByLocationId(int locationId, int clientId)
        {
            var userEntity = _userRepository.GetUserWithRoleById(clientId);
            var userRole = userEntity.UserRole;

            if (userRole.Name == UserRole.Client)
            {
                var allSittersEntity = _userRepository.GetAllSittersByLocationId(locationId);
                var allSittersIsActiveEntity = allSittersEntity.Where(s => s.SitterWorks.Any(sw => sw.LocationsWork.Any(lw => !lw.IsNotActive && lw.LocationId == locationId))).ToList();
                var usersShortsResponse = _mapper.Map<List<UserShortResponse>>(allSittersIsActiveEntity);

                return usersShortsResponse;
            }
            else
            {
                // _logger.Log(LogLevel.Debug, $"{nameof(UserService)} {nameof(GetAllSittersForClientByLocationId)} User with id {clientId} does not have nessety role for get List os Sitters");
                throw new ArgumentException($"User with id {clientId} does not have nessety role for get List os Sitters");
            }
        }
    }
}
