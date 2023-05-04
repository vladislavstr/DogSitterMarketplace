using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceDal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static UserContext _context;

        private static WorkContext _contextWork;

        private readonly ILogger _logger;

        public UserRepository(UserContext context, WorkContext contextWork, ILogger nLogger)
        {
            _context = context;
            _contextWork = contextWork;
            _logger = nLogger;
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
                _logger.Log(LogLevel.Error, $"User with id {id} not found");
                throw new FileNotFoundException($"User with id {id} not found");
            }
        }

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
                _logger.Log(LogLevel.Debug, $"{nameof(UserRepository)} {nameof(GetUserWithRoleById)} {nameof(UserEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }

        public UserEntity AddUser(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            _logger.Log(LogLevel.Info, $"Add new User {user.ToString()}");

            return _context.Users
                .Include(u => u.UserPassportData)
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                    //.Include(u => u.Pets)
                    .Single(u => u.Id == user.Id);
        }

        public UserPassportDataEntity AddUserPassportData(UserPassportDataEntity passportData)
        {
            _context.UsersPassportData.Add(passportData);
            _context.SaveChanges();

            _logger.Log(LogLevel.Info, $"Add new UserPassportData {passportData.ToString()}");

            return _context.UsersPassportData
                    .Single(upd => upd.Id == passportData.Id);
        }

        public UserStatusEntity AddUserStatus(UserStatusEntity userStatus)
        {
            _context.UsersStatuses.Add(userStatus);
            _context.SaveChanges();

            _logger.Log(LogLevel.Info, $"Add new UserStatus {userStatus.ToString()}");

            return _context.UsersStatuses
                    .Single(us => us.Id == userStatus.Id);
        }

        public void DeleteUserById(int id)
        {
            try
            {
                var user = _context.Users.Single(u => !u.IsDeleted && u.Id == id);
                user.IsDeleted = true;
                _context.SaveChanges();

                _logger.Log(LogLevel.Info, $"User with id {id} is deleted");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"User with id {id} not found");
                throw new FileNotFoundException($"User with id {id} not found");
            }
        }

        public void BlockingUserById(int id)
        {
            try
            {
                var user = _context.Users.Single(u => !u.IsDeleted && u.Id == id);
                user.UserStatusId = 2;
                _context.SaveChanges();

                _logger.Log(LogLevel.Info, $"User with id {id} is banned");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $"User with id {id} not found");
                throw new FileNotFoundException($"User with id {id} not found");
            }
        }

        public UserEntity UpdateUserById(UserEntity user)
        {
            var daseUser = _context.Users
                .Include(u => u.UserPassportData)
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                .Single(u => !u.IsDeleted && u.Id == user.Id);

            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"{nameof(UserEntity)} with id {user.Id} not found.");
                throw new NotFoundException(user.Id, nameof(UserEntity));
            }

            daseUser.UserPassportDataId = user.UserPassportDataId;
            daseUser.UserStatusId = user.UserStatusId;

            _context.SaveChanges();

            return _context.Users
                .Include(u => u.UserPassportData)
                .Include(u => u.UserRole)
                .Include(u => u.UserStatus)
                .Single(u => u.Id == user.Id);
        }

        public async Task<UserRoleEntity> GetUserRoleById(int id)
        {
            try
            {
                return await _context.UsersRoles.SingleAsync(ur => ur.Id == id);
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(UserRepository)} {nameof(GetUserRoleById)} {(nameof(UserRoleEntity))} with id {id} not found");
                throw new NotFoundException(id, nameof(UserRole));
            }
        }

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
                _logger.Log(LogLevel.Debug, $"{nameof(UserRepository)} {nameof(GetExistAndNotDeletedUserById)} {(nameof(UserEntity))} with id {id} not found");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }

        public async Task<List<UserEntity>> GetAllSittersByLocationId(int locationId)
        {
            try
            {
                var location = await _contextWork.Locations.SingleOrDefaultAsync(l => l.Id == locationId);

                if (location == null)
                {
                    _logger.Log(LogLevel.Debug, $"{nameof(UserRepository)} {nameof(GetAllSittersByLocationId)} {(nameof(LocationEntity))} with id {locationId} not found");
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
                _logger.Log(LogLevel.Debug, $"({ex}, {nameof(UserRepository)} {nameof(GetAllSittersByLocationId)} ");
                throw new ArgumentException();
            }
        }
    }
}