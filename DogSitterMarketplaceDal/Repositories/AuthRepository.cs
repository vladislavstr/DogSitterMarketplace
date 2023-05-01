using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.IRepositories;

namespace DogSitterMarketplaceDal.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthContext context;

        public AuthRepository(AuthContext authContext)
        {
            context = authContext;
        }

        public int AddUser(UserEntity user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }
    }
}
