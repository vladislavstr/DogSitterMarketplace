using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class AppealRepository : IAppealRepository
    {

        private static AppealContext _context;

        public AppealRepository()
        {
            _context = new AppealContext();
        }

        public IEnumerable<AppealEntity> GetAllAppeals()
        {
            //return _context.Appeals.Where(t => !t.IsDeleted).ToList();
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
                    //.Include(u => u.Pets)
                .Single(a => a.Id == id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception($"Id:{id} - отсутствует");
            }
        }

        public AppealEntity GetAppealByUserId(int id)
        {
            try
            {
                return _context.Appeals
                .Include(a => a.Type)
                .Include(a => a.Status)
                .Include(a => a.Order)
                .Include(a => a.AppealFromUser)
                .Include(a => a.AppealToUser)
                //.Include(u => u.Pets)
                .Single(a => a.Id == id);
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
    }
}