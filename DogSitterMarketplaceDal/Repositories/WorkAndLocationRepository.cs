using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using DogSitterMarketplaceCore.Exceptions;

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

        //прописать 2й логгер
        public SitterWorkEntity GetNotDeletedSitterWorkById(int id)
        {
            try
            {
                return _context.SitterWorks.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                // logger.LogDebug($"{nameof(SitterWorkEntity)} with id {id} not found.");
              //  _logger.Log(LogLevel.Debug, $"{nameof(SitterWorkEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(SitterWorkEntity));
            }
        }

        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id)
        {
            return _context.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.User)
                .Include(sw => sw.LocationWork)
                .ThenInclude(lw => lw.TimingLocationWorks)
                .ThenInclude(tlw => tlw.DayOfWeek)
                .Where(sw => sw.User.Id == id).ToList();
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

        // 2й логгер прописать
        public LocationEntity GetLocationById(int id)
        {
            try
            {
                return _context.Locations.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(LocationEntity)} with id {id} not found.");
               // _logger.Log(LogLevel.Debug, $"{nameof(LocationEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(LocationEntity));
            }
        }
    }
}
