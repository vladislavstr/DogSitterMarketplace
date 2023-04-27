using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceDal.Repositories
{
    public class WorkAndLocationRepository : IWorkAndLocationRepository
    {
        private WorkContext _workContext;
        private ILogger _logger;

        public WorkAndLocationRepository(ILogger logger, WorkContext context)
        {
            _workContext = context;
            _logger = logger;
        }

        public bool AddNewLocationWork(LocationWorkEntity locationWork)
        {
            _workContext.LocationWorks.Add(locationWork);
            _workContext.SaveChanges();
            _logger.Log(LogLevel.Info, $"Add new location work {locationWork.ToString()}");

            return true;
        }

        public bool UpdateLocationWork(LocationWorkEntity updateLocationWork)
        {
            bool result = false;

            var location = _workContext.LocationWorks
                .Include(l => l.Location)
                .Include(l => l.TimingLocationWorks)
                .Include(l => l.SitterWork)
                .SingleOrDefault(l => l.Id == updateLocationWork.Id);

            if (location == null)
            {
                _logger.Log(LogLevel.Error, $"Location work not fount by id {updateLocationWork.Id} ");
                throw new FileNotFoundException($"Location work not fount by id {updateLocationWork.Id} ");
            }
            else
            {
                location.IsNotActive = updateLocationWork.IsNotActive;
                location.SitterWorkId = updateLocationWork.SitterWorkId;
                location.LocationId = updateLocationWork.LocationId;
                location.Price = updateLocationWork.Price;
                _workContext.SaveChanges();
                _logger.Log(LogLevel.Info, $"location work is update {location.ToString()}");
                result = true;
            }

            return result;
        }

        public bool DeleteLocationWork(int locationworkId)
        {
            bool result = false;

            var deleteLocationWork = _workContext.LocationWorks
                .SingleOrDefault(l => l.Id == locationworkId);

            if (deleteLocationWork == null)
            {
                _logger.Log(LogLevel.Error, $"Location work not fount by id {locationworkId} ");
                throw new FileNotFoundException($"Location work not fount by id {locationworkId} ");
            }
            else
            {
                _workContext.LocationWorks.Remove(deleteLocationWork);
                _workContext.SaveChanges();
                _logger.Log(LogLevel.Info, $"location work {deleteLocationWork.ToString()} delete");
                result = true;
            }

            return result;
        }

        public List<LocationWorkEntity> GetAllLocationWork()
        {
            var sitterWorks = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek).ToList();

            if (sitterWorks == null)
            {
                _logger.Log(LogLevel.Warn, $"Time interval not found");
            }

            _logger.Log(LogLevel.Info, $"Загружаемые локации по запросу {nameof(GetAllLocationWork)}");

            return sitterWorks;
        }

        public LocationWorkEntity GetLocationWorkByid(int id)
        {
            var location = _workContext.LocationWorks
                .Include(lw => lw.TimingLocationWorks)
                .ThenInclude(tl => tl.DayOfWeek)
                .Include(lw => lw.Location)
                .Include(lw => lw.SitterWork)
                .SingleOrDefault(lw => lw.Id == id);

            if (location == null)
            {
                _logger.Log(LogLevel.Warn, $"Time interval with id {id} not found");
                throw new FileNotFoundException($"Time interval with id {id} not found");
            }

            return location;
        }

        public List<LocationWorkEntity> GetAllLocationsWorkBySitterWork(int sitterWorkId)
        {
            if (_workContext.SitterWorks.SingleOrDefault(sw => sw.Id == sitterWorkId) == null)
            {
                _logger.Log(LogLevel.Warn, $"Sitter Work  for id {sitterWorkId} not found");
                throw new FileNotFoundException($"Sitter Work  for id {sitterWorkId} not found");
            }

            var sitterWorks = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Where(sw => sw.SitterWorkId == sitterWorkId).ToList();

            if (sitterWorks == null)
            {
                _logger.Log(LogLevel.Warn, $"No Time interval for sitter work {sitterWorkId} ");
            }

            return sitterWorks;
        }

        //прописать 2й логгер
        public SitterWorkEntity GetNotDeletedSitterWorkById(int id)
        {
            try
            {
                return _workContext.SitterWorks.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                // logger.LogDebug($"{nameof(SitterWorkEntity)} with id {id} not found.");
                //  _logger.Log(LogLevel.Debug, $"{nameof(SitterWorkEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(SitterWorkEntity));
            }
        }
        public List<LocationWorkEntity> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false)
        {
            if (_workContext.SitterWorks.SingleOrDefault(sw => sw.Id == sitterWorkId) == null)
            {
                _logger.Log(LogLevel.Warn, $"Sitter Work  for id {sitterWorkId} not found");
                throw new FileNotFoundException($"Sitter Work  for id {sitterWorkId} not found");
            }

            var sitterWorks = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Where(sw => sw.SitterWorkId == sitterWorkId && sw.IsNotActive == isNotActive).ToList();

            if (sitterWorks == null)
            {
                _logger.Log(LogLevel.Warn, $"No Time interval for sitter work {sitterWorkId} ");
            }

            return sitterWorks;
        }

        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id)
        {
            return _workContext.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.User)
                .Include(sw => sw.LocationWork)
                .ThenInclude(lw => lw.TimingLocationWorks)
                .ThenInclude(tlw => tlw.DayOfWeek)
                .Where(sw => sw.User.Id == id).ToList();
        }

        public List<LocationWorkEntity> GetAllLocationWorkByLocation(int locationId)
        {
            if (_workContext.LocationWorks.SingleOrDefault(sw => sw.Id == locationId) == null)
            {
                _logger.Log(LogLevel.Warn, $"Location by id {locationId} not found");
                throw new FileNotFoundException($"Location by id {locationId} not found");
            }

            var sitter = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Include(sw => sw.SitterWork)
                .Where(sw => sw.LocationId == locationId).ToList();

            if (sitter == null)
            {
                _logger.Log(LogLevel.Warn, $"Location work by location {locationId} not found");
            }

            return sitter;
        }

        // 2й логгер прописать
        public LocationEntity GetLocationById(int id)
        {
            try
            {
                return _workContext.Locations.Single(o => o.Id == id && !o.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                //_logger.LogDebug($"{nameof(LocationEntity)} with id {id} not found.");
                // _logger.Log(LogLevel.Debug, $"{nameof(LocationEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(LocationEntity));
            }
        }

        public List<LocationWorkEntity> GetAllLocationsWorkByLocationAndStatus(int locationId, bool isNotActive = false)
        {
            if (_workContext.LocationWorks.SingleOrDefault(sw => sw.Id == locationId) == null)
            {
                _logger.Log(LogLevel.Warn, $"Location by id {locationId} not found");
                throw new FileNotFoundException($"Location by id {locationId} not found");
            }

            var sitter = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Include(sw => sw.SitterWork)
                .Where(sw => sw.LocationId == locationId && sw.IsNotActive == isNotActive).ToList();

            if (sitter == null)
            {
                _logger.Log(LogLevel.Warn, $"Location work by location {locationId} not found");
                throw new FileNotFoundException($"Location work by location {locationId} not found");
            }

            return sitter;
        }

        public List<LocationWorkEntity> GetAllLocationWorkbyActiveStatus(bool isNotActive = false)
        {
            var sitterWorks = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Where(sw => sw.IsNotActive == isNotActive).ToList();

            return sitterWorks;
        }

    }
}
