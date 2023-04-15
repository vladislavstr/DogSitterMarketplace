using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IUserService
    {
        public ICollection<UserResponse> GetAllUsers();

        //public UserResponse GetUser(UserRequest user);

        public UserResponse AddUser(UserRequest user);

        public void DeleteUserById(int id);
    }
}
