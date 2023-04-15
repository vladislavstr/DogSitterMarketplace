using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IUserRepository
    {
        public ICollection<UserEntity> GetAllUsers();

        public UserEntity GetUserById(int id);

        public UserEntity AddUser(UserEntity user);

        public void DeleteUserById(int id);
    }
}
