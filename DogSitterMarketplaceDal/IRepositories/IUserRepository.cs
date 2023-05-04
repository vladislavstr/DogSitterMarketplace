using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IUserRepository
    {
        public List<UserEntity> GetAllUsers();

        public UserEntity GetUserById(int id);

        public UserEntity AddUser(UserEntity user);

        public UserPassportDataEntity AddUserPassportData(UserPassportDataEntity PassportData);

        public UserStatusEntity AddUserStatus(UserStatusEntity userStatus);

        public void DeleteUserById(int id);

        public void BlockingUserById(int id);

        //public void UpdateUserById(UserEntity user);
        public Task<UserRoleEntity> GetUserRoleById(int id);

        public Task<UserEntity> GetUserWithRoleById(int id);

        public Task<UserEntity> GetExistAndNotDeletedUserById(int id);

        public Task<List<UserEntity>> GetAllSittersByLocationId(int locationId);
    }
}
