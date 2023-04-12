using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class UserRepository
    {
        private static UserContext _context;

        public UserRepository()
        {
            _context = new UserContext();
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return _context.Users.Where(t => !t.IsDeleted).ToList();
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
    }
}