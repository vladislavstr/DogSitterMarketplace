using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static UserContext _context;

        private static WorkContext _contextWork;

        public UserRepository(UserContext context, WorkContext contextWork)
        {
            _context = context;
            _contextWork = contextWork;
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

        //2 логгер оставить
        public async Task<UserEntity> GetUserWithRoleById(int id)
        {
            try
            {
                return await _context.Users
                                .Include(u => u.UserRole)
                                .SingleAsync(u => u.Id == id && !u.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                //  _logger.LogDebug($"{nameof(PetRepository)} {nameof(GetExistAndNotDeletedUserById)} {nameof(UserEntity)} with id {id} not found.");
                //_logger.Log(LogLevel.Debug, $"{nameof(PetRepository)} {nameof(GetUserById)} {nameof(UserEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(UserEntity));
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

        //2 логгер прописать
        public async Task<UserRoleEntity> GetUserRoleById(int id)
        {
            try
            {
                return await _context.UsersRoles.SingleAsync(ur => ur.Id == id);
            }
            catch (InvalidOperationException)
            {
                // _logger.Log(LogLevel.Debug, $" {(nameof(UserEntity))} with id {id} not found");
                throw new NotFoundException(id, nameof(UserRole));
            }
        }

        //2 логгер прописать
        public async Task<UserEntity> GetExistAndNotDeletedUserById(int id)
        {
            try
            {
                return await _context.Users
                            .Include(u => u.UserRole)
                            .SingleAsync(u => u.Id == id && !u.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                //_logger.LogDebug($"{nameof(UserEntity)} with id {id} not found.");
                // _logger.Log(LogLevel.Debug, $" {(nameof(UserEntity))} with id {id} not found");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }

        //2 логгер прописать
        public async Task<List<UserEntity>> GetAllSittersByLocationId(int locationId)
        {
            try
            {
                var location = await _contextWork.Locations.SingleOrDefaultAsync(l => l.Id == locationId);

                if (location == null)
                {
                    // _logger.Log(LogLevel.Debug, $" {(nameof(LocationEntity))} with id {locationId} not found");
                    throw new NotFoundException(locationId, nameof(LocationEntity));
                }

                return await _context.Users
                                .Include(u => u.UserRole)
                                .Include(u => u.SitterWorks)
                                .ThenInclude(sw => sw.LocationWork)
                                 .Include(u => u.SitterWorks)
                                .ThenInclude(sw => sw.WorkType)
                                .Where(u => u.UserRole.Name == UserRole.Sitter
                                       && u.SitterWorks.Any(sw => sw.LocationWork.Any(l => l.LocationId == locationId)))
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogDebug($"{ex}, {nameof(CommentRepository)} {nameof(CommentEntity)} {nameof(AddComment)}");
                //_logger.Log(LogLevel.Debug, $"({ex}, {nameof(UserRepository)} {nameof(GetAllSittersByLocationId)} ");
                throw new ArgumentException();
            }
        }
    }
}