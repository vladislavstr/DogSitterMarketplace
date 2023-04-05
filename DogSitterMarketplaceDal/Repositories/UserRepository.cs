using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.Repositories
{
    public class UserRepository
    {
        private static UserContext context;

        public UserRepository()
        {
            context = new UserContext();
        }

        public UserEntity CreateUser(UserEntity user)
        {
            var userDal = new UserEntity
            {

                Email = user.Email,

                Password = user.Password,

                PhoneNumber = user.PhoneNumber,

                Name = user.Name,
            };

            context.User.Add(userDal);
            context.SaveChanges();

            return new UserEntity
            {
                Id = userDal.Id,

                Email = userDal.Email,

                Password = userDal.Password,

                PhoneNumber = userDal.PhoneNumber,

                Name = userDal.Name,
            };
        }

        public IEnumerable<UserEntity> GetUser()
        {
            return context.User.Where(t => !t.IsDeleted).ToList();
        }
    }
}