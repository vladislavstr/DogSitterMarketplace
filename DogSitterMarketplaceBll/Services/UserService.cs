using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Pets.Response;
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

        public ICollection<UserResponse> GetAllUsers()
        {
            var allusersEntitys = _userRepository.GetAllUsers();
            var usersEntitys = allusersEntitys
                           .Where(u => !u.IsDeleted);
            var userResponse = _mapper.Map<ICollection<UserResponse>>(usersEntitys);

            return userResponse;
        }

        public UserResponse AddUser(UserRequest user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            var addUserEntity = _userRepository.AddUser(userEntity);
            var addUserResponse = _mapper.Map<UserResponse>(addUserEntity);

            return addUserResponse;
        }

        public void DeleteUserById(int id)
        {
            _userRepository.DeleteUserById(id);
        }
    }
}
