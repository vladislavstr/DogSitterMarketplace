using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IUserRepository
    {
        public List<UserEntity> GetAllUsers();

        public UserEntity GetUserById(int id);

        public UserEntity AddUser(UserEntity user);

        public void DeleteUserById(int id);

        //public void UpdateUserById(UserEntity user);
        public UserRoleEntity GetUserRoleById(int id);

        public UserEntity GetUserWithRoleById(int id);

        public UserEntity GetExistAndNotDeletedUserById(int id);

        public List<UserEntity> GetAllSittersByLocationId(int locationId);
    }
}
