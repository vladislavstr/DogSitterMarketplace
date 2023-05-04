using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceDal.Repositories
{
    public class TimeWorkRepository : ITimeWorkRepository
    {
        private static WorkContext _workContext;
        private static ILogger _logger;

        public TimeWorkRepository(ILogger logger, WorkContext context)
        {
            _workContext = context;
            _logger = logger;
        }

        public async Task<List<TimingLocationWorkEntity>> AddNewTimingsLocation(List<TimingLocationWorkEntity> timings)
        {
            await _workContext.TimingLocationWorks.AddRangeAsync(timings);
            await _workContext.SaveChangesAsync();
            List<TimingLocationWorkEntity> result = new List<TimingLocationWorkEntity>();

            foreach (var t in timings)
            {
                _logger.Log(LogLevel.Debug, $"Add new timings {t.ToString()}");
            }

            foreach (var t in timings)
            {
                result.Add(await _workContext.TimingLocationWorks
                    .Include(tl => tl.LocationWork)
                    .Include(tl => tl.DayOfWeek)
                    .SingleOrDefaultAsync(tl => tl.Id == t.Id));
            }

            return result;
        }

        public async Task<TimingLocationWorkEntity> AddNewTimingLocation(TimingLocationWorkEntity timing)
        {
            await _workContext.TimingLocationWorks.AddAsync(timing);
            await _workContext.SaveChangesAsync();
            _logger.Log(LogLevel.Debug, $"Add new timings {timing.ToString()}");
            var result = await _workContext.TimingLocationWorks
                    .Include(tl => tl.LocationWork)
                    .Include(tl => tl.DayOfWeek)
                    .SingleOrDefaultAsync(tl => tl.Id == timing.Id);

            return result;
        }

        public async Task<TimingLocationWorkEntity> UpdateTimingLocation(TimingLocationWorkEntity timing)
        {
            var updateTiming = await _workContext.TimingLocationWorks
                .Include(tl => tl.DayOfWeek)
                .Include(tl => tl.LocationWork)
                .SingleOrDefaultAsync(tl => tl.Id == timing.Id);

            if (updateTiming != null)
            {
                updateTiming.Start = timing.Start;
                updateTiming.Stop = timing.Stop;
                updateTiming.DayOfWeekId = timing.DayOfWeekId;
                updateTiming.LocationWorkId = timing.LocationWorkId;
                await _workContext.SaveChangesAsync();
                _logger.Log(LogLevel.Debug, $"Update timings {updateTiming.ToString()}");
            }
            else
            {
                _logger.Log(LogLevel.Error, $"Time interval with id {timing.Id} not found");
                throw new FileNotFoundException($"Time interval with id {timing.Id} not found");
            }

            return updateTiming;
        }

        public async Task<List<DayOfWeekEntity>> GetDaysOfWeek()
        {
            return await _workContext.DaysOfWeek.ToListAsync();
        }

        public TimingLocationWorkEntity GetTiming(int idTiming)
        {
            var timing = _workContext.TimingLocationWorks
                .Include(t => t.DayOfWeek)
                .Include(t => t.LocationWork)
                .SingleOrDefault(t => t.Id == idTiming);

            if (timing == null)
            {
                _logger.Log(LogLevel.Error, $"Time interval with id {timing.Id} not found");
                throw new FileNotFoundException($"Time interval with id {timing.Id} not found");
            }

            return timing;
        }

        public async Task<List<TimingLocationWorkEntity>> GetAllTimigsOfLocationWork(int locationWorkId)
        {
            if (!await _workContext.LocationWorks.AnyAsync(lw => lw.Id == locationWorkId))
            {
                _logger.Log(LogLevel.Error, $"Location work by id {locationWorkId} not found");
                throw new FileNotFoundException($"Location work by id {locationWorkId} not found");
            }

            List<TimingLocationWorkEntity> oldTimings = new List<TimingLocationWorkEntity>();

            oldTimings = await _workContext.TimingLocationWorks
                .Include(t => t.LocationWork)
                .Include(t => t.DayOfWeek)
                .Where(t => t.LocationWorkId == locationWorkId).ToListAsync();

            return oldTimings;
        }

        public async Task<bool> DeleteTiming(int id)
        {
            bool isDelete = false;

            var deleteTiming = await _workContext.TimingLocationWorks
                .SingleOrDefaultAsync(t => t.Id == id);

            if (deleteTiming != null)
            {
                _workContext.TimingLocationWorks.Remove(deleteTiming);
                await _workContext.SaveChangesAsync();
                _logger.Log(LogLevel.Debug, $"Time interval with id {id} is delete complete");
                isDelete = true;
            }
            else
            {
                _logger.Log(LogLevel.Error, $"Time interval with id {id} not found");
                throw new FileNotFoundException($"Time interval with id {id} not found");
            }

            return isDelete;
        }

        public async Task<bool> DeleteTimingByLocationWork(int locationWorkId)
        {
            if (!await _workContext.LocationWorks.AnyAsync(lw => lw.Id == locationWorkId))
            {
                _logger.Log(LogLevel.Error, $"Time interval with location work id {locationWorkId} not found");
                throw new FileNotFoundException($"Time interval location work id {locationWorkId} not found");
            }

            bool isDelete = false;

            var deleteTiming = await _workContext.TimingLocationWorks
                .Where(t => t.LocationWorkId == locationWorkId).ToListAsync();

            if (deleteTiming != null)
            {
                _workContext.TimingLocationWorks.RemoveRange(deleteTiming);
                await _workContext.SaveChangesAsync();
                _logger.Log(LogLevel.Info, $"Time interval with location work id {locationWorkId} is delete");
                isDelete = true;
            }
            else
            {
                _logger.Log(LogLevel.Warn, $"Time interval with location work id {locationWorkId} not found");
            }

            return isDelete;
        }
    }
}
