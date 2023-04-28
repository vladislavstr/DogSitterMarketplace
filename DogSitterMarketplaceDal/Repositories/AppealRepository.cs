using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DogSitterMarketplaceDal.Repositories
{
    public class AppealRepository : IAppealRepository
    {

        private static AppealContext _context;

        public AppealRepository(AppealContext context)
        {
            _context = context;
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

        public void UpdateAppealStatusById(int AppealId, int StatusId)
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

        public AppealEntity DoResponseTextByAppeal(AppealEntity appeal)
        {
            try
            {
                var appealDb = _context.Appeals.Single(a => a.Id == appeal.Id);
                appealDb.ResponseText = appeal.ResponseText;
                appealDb.StatusId = appeal.StatusId;
                appealDb.DateOfResponse = appeal.DateOfResponse;
                _context.SaveChanges();

                return appealDb;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{appeal.Id} - отсутствует");
            }
        }
    }
}