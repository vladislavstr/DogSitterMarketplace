using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public ICollection<UserEntity> GetAllUsers()
        {
            return _context.Users.Where(t => !t.IsDeleted).ToList();
        }

        public UserEntity GetUserById(int id)
        {
            try
            {
                return _context.Users
                    .Include(u => u.PassportData)
                    .Include(u => u.Role)
                    .Include(u => u.Status)
                    .Include(u => u.Pets)
                    .Single(u => u.Id == id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }

        }

        public UserEntity AddUser(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return _context.Users
                .Include(u => u.PassportData)
                .Include(u => u.Role)
                .Include(u => u.Status)
                .Include(u => u.Pets)
                .Single(u=>u.Id == user.Id);
        }

        public void DeleteUserById(int id)
        {
            try
            {
                var user = _context.Users.Single(u => !u.IsDeleted && u.Id == id);
                user.IsDeleted = true;
                _context.SaveChanges();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}