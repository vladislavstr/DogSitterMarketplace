using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IUserRepository
    {
        public ICollection<UserEntity> GetAllUsers();

        public UserEntity AddUser(UserEntity user);
    }
}
