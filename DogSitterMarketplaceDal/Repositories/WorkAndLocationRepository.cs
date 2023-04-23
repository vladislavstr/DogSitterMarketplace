using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Contexts;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class WorkAndLocationRepository : IWorkAndLocationRepository
    {
        private static WorkContext _context;

        public WorkAndLocationRepository()
        {
            _context = new WorkContext();
        }

        public LocationWorkEntity GetLocationWorkByid(int id)
        {
            var location = _context.LocationWorks
                .Include(lw => lw.TimingLocationWorks)
                .Include(lw => lw.Location)
                .Include(lw => lw.SitterWork)
                .SingleOrDefault(lw => lw.Id == id);

            return location;
        }

        public SitterWorkEntity GetSitterWorkByItsId(int id)
        {
            var sitterWork = _context.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.LocationWork)
                .ThenInclude(sw => sw.Location)
                .Include(sw => sw.LocationWork)
                .ThenInclude(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .AsNoTracking()
                .SingleOrDefault(sw => sw.IsDeleted == false && sw.Id == id);

            return sitterWork;
        }


        public List<SitterWorkEntity> GetSitterWorksByUserId(int id)
        {
            var result = _context.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.LocationWork)
                .ThenInclude(sw => sw.Location)
                .Where(sw => sw.UserId == id).ToList();

            return result;
        }
    }
}
