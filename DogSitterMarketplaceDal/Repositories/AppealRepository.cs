using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using Microsoft.EntityFrameworkCore;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceDal.Repositories
{
    public class AppealRepository : IAppealRepository
    {

        private static AppealContext _context;
        private static ILogger _logger;

        public AppealRepository(AppealContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.Log(LogLevel.Error, $"Appeal with id {id} not found");
                throw new FileNotFoundException($"Appeal with id {id} not found");
            }
        }

        public IEnumerable<AppealStatusEntity> GetAllAppealStatuses()
        {
            var result = new List<AppealStatusEntity>();

            result = _context.AppealsStatuses
                .AsNoTracking()
                .ToList();

            return result;
        }

        public IEnumerable<AppealTypeEntity> GetAllAppealTypes()
        {
            var result = new List<AppealTypeEntity>();

            result = _context.AppealsTypes
                .AsNoTracking()
                .ToList();

            return result;
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
                _logger.Log(LogLevel.Error, $"Appeal to user id {id} not found");
                throw new FileNotFoundException($"Appeal to user id {id} not found");
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
                _logger.Log(LogLevel.Error, $"Appeal from user id {id} not found");
                throw new FileNotFoundException($"Appeal from user id {id} not found");
            }
        }

        public AppealEntity AddAppeal(AppealEntity appeal)
        {
            _context.Appeals.Add(appeal);
            _context.SaveChanges();

            _logger.Log(LogLevel.Info, $"Add new Appeal {appeal.ToString()}");

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

            _logger.Log(LogLevel.Info, $"Add new AppealStatus {appealStatus.ToString()}");

            return appealStatus;
        }

        public AppealTypeEntity AddAppealType(AppealTypeEntity appealType)
        {
            _context.AppealsTypes.Add(appealType);
            _context.SaveChanges();

            _logger.Log(LogLevel.Info, $"Add new AppealType {appealType.ToString()}");

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
                _logger.Log(LogLevel.Error, $"Appeal with id {AppealId} not found");
                throw new FileNotFoundException($"Appeal with id {AppealId} not found");
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
                _logger.Log(LogLevel.Error, $"Appeal with id {appeal.Id} not found");
                throw new FileNotFoundException($"TAppeal with id {appeal.Id} not found");
            }
        }
    }
}