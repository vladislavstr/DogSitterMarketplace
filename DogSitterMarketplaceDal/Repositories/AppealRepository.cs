using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.Models.Appeals;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class AppealRepository
    {

        private static AppealContext _context;

        public AppealRepository()
        {
            _context = new AppealContext();
        }

        public IEnumerable<AppealEntity> GetAllAppeal()
        {
            return _context.Appeals.Where(t => !t.IsDeleted).ToList();
        }

        public AppealEntity AddUser(AppealEntity appeal)
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
    }
}