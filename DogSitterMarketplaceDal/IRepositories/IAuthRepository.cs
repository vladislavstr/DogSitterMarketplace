using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IAuthRepository
    {
        int AddUser(UserEntity user);
    }
}
