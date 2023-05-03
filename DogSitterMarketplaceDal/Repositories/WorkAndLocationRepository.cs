using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DogSitterMarketplaceDal.Repositories
{
    public class WorkAndLocationRepository : IWorkAndLocationRepository
    {
        private static WorkContext _context;

        private readonly ILogger _logger;

        public WorkAndLocationRepository(ILogger nLogger)
        {
            _context = new WorkContext();
            _logger = nLogger;
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

        public async Task<SitterWorkEntity> GetNotDeletedSitterWorkById(int id)
        {
            try
            {
                return await _context.SitterWorks
                    .Include(sw => sw.LocationWork)
                    .SingleAsync(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(WorkAndLocationRepository)} {nameof(GetNotDeletedSitterWorkById)} {nameof(SitterWorkEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(SitterWorkEntity));
            }
        }

        public async Task<List<SitterWorkEntity>> GetAllSitterWorksByUserId(int id)
        {
            return await _context.SitterWorks
                            .Include(sw => sw.WorkType)
                            .Include(sw => sw.User)
                            .Include(sw => sw.LocationWork)
                            .ThenInclude(lw => lw.TimingLocationWorks)
                            .ThenInclude(tlw => tlw.DayOfWeek)
                            .Where(sw => sw.User.Id == id).ToListAsync();
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

        public async Task<LocationEntity> GetLocationById(int id)
        {
            try
            {
                return await _context.Locations.SingleAsync(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(WorkAndLocationRepository)} {nameof(GetLocationById)} {nameof(LocationEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(LocationEntity));
            }
        }

        public async Task<List<SitterWorkEntity>> GetSittersWorksByThemId(List<int> sittersWorksId)
        {
            try
            {
                if (!sittersWorksId.Any())
                {
                    return new List<SitterWorkEntity>();
                }

                List<SitterWorkEntity> result = new List<SitterWorkEntity>();

                foreach (var id in sittersWorksId)
                {
                    result.Add(await _context.SitterWorks.SingleAsync(sw => sw.Id == id));
                }

                return result;
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(WorkAndLocationRepository)} {nameof(GetSittersWorksByThemId)} {nameof(LocationEntity)} One or more id from {sittersWorksId} does not found");
                throw new ArgumentException($"One or more id from {sittersWorksId} does not found");
            }
        }
    }
}
