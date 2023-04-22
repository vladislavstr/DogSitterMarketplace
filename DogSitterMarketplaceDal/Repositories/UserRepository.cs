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

        public List<UserEntity> GetAllUsers()
        {
            
            var result = new List<UserEntity>();

            result = _context.Users
                .Include(u => u.UserPassportData)
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                .AsNoTracking()
                .ToList();

            return result;
        }

        public UserEntity GetUserById(int id)
        {
            try
            {
                return _context.Users
                .Include(u => u.UserPassportData)
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                    //.Include(u => u.Pets)
                    .Single(u => u.Id == id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }

        public UserEntity AddUser(UserEntity user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return _context.Users
                .Include(u => u.UserPassportData)
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                    //.Include(u => u.Pets)
                    .Single(u => u.Id == user.Id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new ArgumentException();
            }
        }

        public void DeleteUserById(int id)
        {
            try
            {
                var user = _context.Users.Single(u => !u.IsDeleted && u.Id == id);
                user.IsDeleted = true;
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }

        //public UserEntity UpdateUserById(UserEntity user)
        //{
        //    try
        //    {
        //        if (user != null)
        //        {
        //            var daseUser = _context.Users.Single(u => !u.IsDeleted && u.Id == id);
        //            daseUser.Email = user.Email;
        //            daseUser.Password = user.Password;
        //            daseUser.PhoneNumber = user.PhoneNumber;
        //            daseUser.Name = user.Name;
        //            daseUser.PassportData = user.PassportData;
        //            daseUser.Role = user.Role;
        //            daseUser.Status = user.Status;
        //            daseUser.Pets = user.Pets;
        //        }
        //        else
        //        {
        //            throw new Exception($"Id:{user.Name} - отсутствует");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        throw new ArgumentException();
        //    }
        //}

    }
}