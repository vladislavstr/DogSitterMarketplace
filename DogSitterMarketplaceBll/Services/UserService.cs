using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Users.Response;
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
            var  usersEntitys = allusersEntitys
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
    }
}
