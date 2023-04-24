using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class AppealRepository : IAppealRepository
    {

        private static AppealContext _context;
        private static UserContext _userContext;

        public AppealRepository(AppealContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;

            AppealTypeEntity defaultAppealTypeEntity_1 = new AppealTypeEntity { Name = "жалоба" };
            AppealTypeEntity defaultAppealTypeEntity_2 = new AppealTypeEntity { Name = "отзыв" };
            AppealStatusEntity defaultAppealStatusEntity_1 = new AppealStatusEntity { Name = "на рассмотрении" };
            AppealStatusEntity defaultAppealStatusEntity_2 = new AppealStatusEntity { Name = "отклонено" };
            AppealStatusEntity defaultAppealStatusEntity_3 = new AppealStatusEntity { Name = "ответ получен" };
            UserRoleEntity defaultUserRoleEntity_1 = new UserRoleEntity { Name = "администратор" };
            UserRoleEntity defaultUserRoleEntity_2 = new UserRoleEntity { Name = "ситтер" };
            UserRoleEntity defaultUserRoleEntity_3 = new UserRoleEntity { Name = "клиент" };
            UserStatusEntity defaultUserStatusEntity_1 = new UserStatusEntity { Name = "активен" };
            UserStatusEntity defaultUserStatusEntity_2 = new UserStatusEntity { Name = "заблокирован" };
            UserEntity defaultUserEntity = new UserEntity
            {
                Email = "test@gmail.com",
                Password = "fdfsf124!",
                PhoneNumber = "89999999999",
                Name = "Name",
                IsDeleted = false,
                UserPassportDataId = 1,
                UserRole = defaultUserRoleEntity_2,
                UserRoleId = 1,
                UserStatus = defaultUserStatusEntity_1,
                UserStatusId = 1
            };

            _context.AppealsTypes.Add(defaultAppealTypeEntity_1);
            _context.AppealsTypes.Add(defaultAppealTypeEntity_2);
            _context.AppealsStatuses.Add(defaultAppealStatusEntity_1);
            _context.AppealsStatuses.Add(defaultAppealStatusEntity_2);
            _context.AppealsStatuses.Add(defaultAppealStatusEntity_3);
            _context.UserRole.Add(defaultUserRoleEntity_1);
            _context.UserRole.Add(defaultUserRoleEntity_2);
            _context.UserRole.Add(defaultUserRoleEntity_3);
            _context.UserStatus.Add(defaultUserStatusEntity_1);
            _context.UserStatus.Add(defaultUserStatusEntity_2);
            _context.Users.Add(defaultUserEntity);

            _context.SaveChanges();
        }

        public IEnumerable<AppealEntity> GetAllAppeals()
        {
            var result = new List<AppealEntity>();

            result = _context.Appeals
                .Include(a => a.Type)
                .Include(a => a.Status)
                .Include(a => a.Order)
                .Include(a => a.AppealFromUser)
                .Include(a => a.AppealToUser)
                .AsNoTracking()
                .ToList();

            return result;
        }

        public AppealEntity GetAppealById(int id)
        {
            try
            {
                return _context.Appeals
                .Include(a => a.Type)
                .Include(a => a.Status)
                .Include(a => a.Order)
                .Include(a => a.AppealFromUser)
                .Include(a => a.AppealToUser)
                .Single(a => a.Id == id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }
        public AppealEntity GetAppealByUserIdToWhom(int id)
        {
            try
            {
                return _context.Appeals
                .Include(a => a.Type)
                .Include(a => a.Status)
                .Include(a => a.Order)
                .Include(a => a.AppealFromUser)
                .Include(a => a.AppealToUser)
                .Single(a => a.AppealToUserId == id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }

        public AppealEntity GetAppealByUserIdFromWhom(int id)
        {
            try
            {
                return _context.Appeals
                .Include(a => a.Type)
                .Include(a => a.Status)
                .Include(a => a.Order)
                .Include(a => a.AppealFromUser)
                .Include(a => a.AppealToUser)
                .Single(a => a.AppealFromUserId == id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }


        public AppealEntity AddAppeal(AppealEntity appeal)
        {
            appeal.IsDeleted = false;
            _context.Appeals.Add(appeal);
            _context.SaveChanges();

            return _context.Appeals
                .Include(a => a.Type)
                .Include(a => a.Status)
                .Include(a => a.Order)
                .Include(a => a.AppealFromUser)
                .Include(a => a.AppealToUser)
                .Single(a => a.Id == appeal.Id);
        }

        public AppealStatusEntity AddAppealStatus(AppealStatusEntity appealStatus)
        {
            _context.AppealsStatuses.Add(appealStatus);
            _context.SaveChanges();

            return appealStatus;
        }

        public AppealTypeEntity AddAppealType(AppealTypeEntity appealType)
        {
            _context.AppealsTypes.Add(appealType);
            _context.SaveChanges();

            return appealType;
        }

        public void DeleteAppealById(int id)
        {
            try
            {
                var appeal = _context.Appeals.Single(a => !a.IsDeleted && a.Id == id);
                appeal.IsDeleted = true;
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }

        public void UpdateAppealStatusById(int AppealId, int StatusId )
        {
            try
            {
                var appeal = _context.Appeals.Single(a => a.Id == AppealId);
                appeal.StatusId = StatusId;
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{AppealId} - отсутствует");
            }
        }
    }
}