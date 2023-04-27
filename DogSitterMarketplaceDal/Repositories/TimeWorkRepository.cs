using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Contexts;
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

        public TimeWorkRepository(ILogger logger,WorkContext context)
        {
            _workContext = context;
            _logger = logger;
        }


        public bool AddNewTimingsLocation(List<TimingLocationWorkEntity> timing)
        {
            _workContext.TimingLocationWorks.AddRange(timing);
            _workContext.SaveChanges();

            foreach (var t in timing)
            {
                _logger.Log(LogLevel.Info, $"Add new timings {t.ToString()}");
            }

            return true;
        }

        public bool AddNewTimingLocation(TimingLocationWorkEntity timing)
        {
            _workContext.TimingLocationWorks.Add(timing);
            _workContext.SaveChanges();
            _logger.Log(LogLevel.Info, $"Add new timings {timing.ToString()}");

            return true;
        }

        public bool UpdateTimingLocation(TimingLocationWorkEntity timing)
        {
            bool timingUpdate = false;

            var updateTiming = _workContext.TimingLocationWorks
                .Include(tl => tl.DayOfWeek)
                .Include(tl => tl.LocationWork)
                .SingleOrDefault(tl => tl.Id == timing.Id);

            if (updateTiming != null)
            {
                updateTiming.Start = timing.Start;
                updateTiming.Stop = timing.Stop;
                updateTiming.DayOfWeekId = timing.DayOfWeekId;
                updateTiming.LocationWorkId = timing.LocationWorkId;
                _workContext.SaveChanges();
                timingUpdate = true;
                _logger.Log(LogLevel.Info, $"Update timings {updateTiming.ToString()}");
            }
            else
            {
                _logger.Log(LogLevel.Error, $"Time interval with id {timing.Id} not found");
                throw new FileNotFoundException($"Time interval with id {timing.Id} not found");
            }

            return timingUpdate;
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

        public List<TimingLocationWorkEntity> GetAllTimigsOfLocationWork(int locationWorkId)
        {
            List<TimingLocationWorkEntity> oldTimings = null;

            oldTimings = _workContext.TimingLocationWorks
                .Include(t => t.LocationWork)
                .Include(t => t.DayOfWeek)
                .Where(t => t.LocationWorkId == locationWorkId).ToList();

            return oldTimings;
        }

        public bool DeleteTiming(int id)
        {
            bool isDelete = false;

            var deleteTiming = _workContext.TimingLocationWorks
                .SingleOrDefault(t => t.Id == id);

            if (deleteTiming != null)
            {
                _workContext.TimingLocationWorks.Remove(deleteTiming);
                _workContext.SaveChanges();
                _logger.Log(LogLevel.Info, $"Time interval with id {id} not found");
                isDelete = true;
            }
            else
            {
                _logger.Log(LogLevel.Error, $"Time interval with id {id} not found");
                throw new FileNotFoundException($"Time interval with id {id} not found");
            }

            return isDelete;
        }
    }
}
