using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IUserService
    {
        public List<UserResponse> GetAllUsers();

        public ICollection<UserResponse> GetAllNotDeletedUsers();

        public UserResponse GetUserById(int id);

        public UserResponse AddUser(UserRequest user);

        public UserPassportDataResponse AddUserPassportData(UserPassportDataRequest PassportData);

        public UserStatusResponse AddUserStatus(UserStatusRequest userStatus);

        public void DeleteUserById(int id);

        public void BlockingUserById(int id);

        public Task<List<UserShortLocationWorkResponse>> GetAllSittersByLocationId(int locationId);
    }
}
