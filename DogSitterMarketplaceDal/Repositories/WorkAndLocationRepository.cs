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

        public async Task<LocationWorkEntity> AddNewLocationWork(LocationWorkEntity locationWork)
        {
            _workContext.LocationWorks.Add(locationWork);
            await _workContext.SaveChangesAsync();
            _logger.Log(LogLevel.Info, $"Add new location work {locationWork.ToString()}");

            var returnlocationWork = _workContext.LocationWorks
                .Include(lw => lw.Location)
                .Include(lw=>lw.SitterWork)
                .ThenInclude(lw=>lw.WorkType)
                .Include(lw=>lw.SitterWork)
                .ThenInclude(lw=>lw.User)
                .SingleOrDefault(lw => lw.Id == locationWork.Id);

            return returnlocationWork;
        }

        public async Task<LocationWorkEntity> UpdateLocationWork(LocationWorkEntity updateLocationWork)
        {
            var location = await _workContext.LocationWorks
                .Include(l => l.Location)
                .Include(l=>l.SitterWork)
                .ThenInclude(lw => lw.WorkType)
                .Include(lw => lw.SitterWork)
                .ThenInclude(lw => lw.User)
                .SingleOrDefaultAsync(l => l.Id == updateLocationWork.Id);

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
                await _workContext.SaveChangesAsync();
                _logger.Log(LogLevel.Info, $"location work is update {location.ToString()}");
            }

            return location;
        }

        public async Task<bool> DeleteLocationWork(int locationworkId)
        {
            bool result = false;

            var deleteLocationWork = await _workContext.LocationWorks
                .SingleOrDefaultAsync(l => l.Id == locationworkId);

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

        public List<LocationWorkEntity> GetAllLocationWork(bool? IsNotActive = null)
        {
            var sitterWorks = _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Where(sw=> sw.IsNotActive==IsNotActive || IsNotActive==null).ToList();

            if (sitterWorks == null)
            {
                _logger.Log(LogLevel.Warn, $"Time interval not found");
            }

            _logger.Log(LogLevel.Debug, $"Location works get to request {nameof(GetAllLocationWork)}");

            return sitterWorks;
        }

        public async Task<LocationWorkEntity> GetLocationWorkByid(int id)
        {
            var result = await _workContext.LocationWorks
                .Include(lw => lw.TimingLocationWorks)
                .ThenInclude(tl => tl.DayOfWeek)
                .Include(lw => lw.Location)
                .Include(lw => lw.SitterWork)
                .SingleOrDefaultAsync(lw => lw.Id == id);

            if (result == null)
            {
                _logger.Log(LogLevel.Warn, $"Time interval with id {id} not found");
            }

            return result;
        }

        public async Task<List<LocationWorkEntity>> GetAllLocationsWorkBySitterWork(int sitterWorkId,bool? IsNotActive = null)
        {
            if (! await _workContext.SitterWorks.AnyAsync(sw => sw.Id == sitterWorkId))
            {
                _logger.Log(LogLevel.Warn, $"Sitter Work  for id {sitterWorkId} not found");
                throw new FileNotFoundException($"Sitter Work  for id {sitterWorkId} not found");
            }

            var sitterWorks = await _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Where(sw => (sw.SitterWorkId == sitterWorkId && IsNotActive == null)
            || (sw.SitterWorkId == sitterWorkId && sw.IsNotActive==IsNotActive)).ToListAsync();

            if (sitterWorks == null)
            {
                _logger.Log(LogLevel.Warn, $"No Time interval for sitter work {sitterWorkId} ");
            }

            return sitterWorks;
        }

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

        //public async Task<List<LocationWorkEntity>> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false)
        //{
        //    if (!await _workContext.SitterWorks.AnyAsync(sw => sw.Id == sitterWorkId))
        //    {
        //        _logger.Log(LogLevel.Warn, $"Sitter Work  for id {sitterWorkId} not found");
        //        throw new FileNotFoundException($"Sitter Work  for id {sitterWorkId} not found");
        //    }

        //    var sitterWorks = await _workContext.LocationWorks
        //        .Include(sw => sw.Location)
        //        .Include(sw => sw.TimingLocationWorks)
        //        .ThenInclude(sw => sw.DayOfWeek)
        //        .Where(sw => sw.SitterWorkId == sitterWorkId && sw.IsNotActive == isNotActive).ToListAsync();

        //    if (sitterWorks == null)
        //    {
        //        _logger.Log(LogLevel.Warn, $"No Time interval for sitter work {sitterWorkId}");
        //    }

        //    return sitterWorks;
        //}

        public async Task<List<LocationWorkEntity>> GetAllLocationWorkByLocation(int locationId, bool? IsNotActive = null)
        {
            if (!await _workContext.Locations.AnyAsync(sw => sw.Id == locationId))
            {
                _logger.Log(LogLevel.Warn, $"Location by id {locationId} not found");
                throw new FileNotFoundException($"Location by id {locationId} not found");
            }

            var sitter = await _workContext.LocationWorks
                .Include(sw => sw.Location)
                .Include(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .Include(sw => sw.SitterWork)
                .ThenInclude(sw=>sw.WorkType)
                .Where(sw => (sw.LocationId == locationId && IsNotActive==null)
                || (sw.LocationId == locationId && sw.IsNotActive == IsNotActive)).ToListAsync();

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

        public async Task<List<LocationEntity>> GetAllLocation(bool? IsDeleted = null)
        {
            var result = await _workContext.Locations.
                Where(l=>l.IsDeleted == IsDeleted || IsDeleted==null ).ToListAsync();

            return result;
        }

        //public async Task<List<LocationEntity>> GetAllLocationByStatus(bool isDelete = false)
        //{
        //    var result = await _workContext.Locations
        //        .Where(l => l.IsDeleted == isDelete)
        //        .ToListAsync();

        //    return result;
        //}

        //public async Task<List<LocationWorkEntity>> GetAllLocationsWorkByLocationAndStatus(int locationId, bool isNotActive = false)
        //{
        //    if (!await _workContext.Locations.AnyAsync(sw => sw.Id == locationId))
        //    {
        //        _logger.Log(LogLevel.Warn, $"Location by id {locationId} not found");
        //        throw new FileNotFoundException($"Location by id {locationId} not found");
        //    }

        //    var LocationsWorks = await _workContext.LocationWorks
        //        .Include(sw => sw.Location)
        //        .Include(sw => sw.TimingLocationWorks)
        //        .ThenInclude(sw => sw.DayOfWeek)
        //        .Include(sw => sw.SitterWork)
        //        .Where(sw => sw.LocationId == locationId && sw.IsNotActive == isNotActive).ToListAsync();

        //    if (LocationsWorks.Count==0)
        //    {
        //        _logger.Log(LogLevel.Warn, $"Location work by location {locationId} not found");
        //    }

        //    return LocationsWorks;
        //}

        //public async Task<List<LocationWorkEntity>> GetAllLocationWorkbyActiveStatus(bool isNotActive = false)
        //{
        //    var locationsWorks = await _workContext.LocationWorks
        //        .Include(sw => sw.Location)
        //        .Include(sw => sw.TimingLocationWorks)
        //        .ThenInclude(sw => sw.DayOfWeek)
        //        .Where(sw => sw.IsNotActive == isNotActive).ToListAsync();

        //    if (locationsWorks.Count == 0)
        //    {
        //        _logger.Log(LogLevel.Warn, $"Location work not found by status isNotActive = {isNotActive}");
        //    }

        //    return locationsWorks;
        //}

        public async Task<SitterWorkEntity> AddSitterWork(SitterWorkEntity sitterWork)
        {
            if (!await _workContext.WorkTypes.AnyAsync(w => w.Id == sitterWork.WorkTypeId && w.IsDeleted == false))
            {
                _logger.Log(LogLevel.Error, $"This work type by id {sitterWork.UserId} does not exist or has been deleted");
                throw new FileNotFoundException($"This work type by id {sitterWork.UserId} does not exist or has been deleted");
            }

            if (!await _workContext.Users.AnyAsync(u => u.Id == sitterWork.UserId && u.IsDeleted == false))
            {
                _logger.Log(LogLevel.Error, $"This user by id {sitterWork.UserId} does not exist or has been deleted");
                throw new FileNotFoundException($"This user by id {sitterWork.UserId} does not exist or has been deleted ");
            }

            _workContext.SitterWorks.Add(sitterWork);
            await _workContext.SaveChangesAsync();
            var result =  _workContext.SitterWorks
                .Include(sw => sw.User)
                .Include(sw => sw.WorkType)
                .SingleOrDefault(sw => sw.Id == sitterWork.Id);

            return result;
        }

        public async Task<SitterWorkEntity> UpdateSitterWork(SitterWorkEntity sitterWork)
        {
            if (!await _workContext.WorkTypes.AnyAsync(w => w.Id == sitterWork.WorkTypeId && w.IsDeleted == false))
            {
                _logger.Log(LogLevel.Error, $"This work type by id {sitterWork.UserId} does not exist or has been deleted");
                throw new FileNotFoundException($"This work type by id {sitterWork.UserId} does not exist or has been deleted");
            }

            if (!await _workContext.Users.AnyAsync(u => u.Id == sitterWork.UserId && u.IsDeleted == false))
            {
                _logger.Log(LogLevel.Error, $"This user by id {sitterWork.UserId} does not exist or has been deleted");
                throw new FileNotFoundException($"This user by id {sitterWork.UserId} does not exist or has been deleted ");
            }

            var result = await _workContext.SitterWorks
                .SingleOrDefaultAsync(sw => sw.Id == sitterWork.Id && sw.IsDeleted == false);

            if (result == null)
            {
                _logger.Log(LogLevel.Warn, $"The sitter service has been removed or does not exist");
                throw new FileNotFoundException($"The sitter service has been removed or does not exist ");
            }
            else
            {
                result.Id = sitterWork.Id;
                result.Comment = sitterWork.Comment;
                result.UserId = sitterWork.UserId;
                result.WorkTypeId = sitterWork.WorkTypeId;
                await _workContext.SaveChangesAsync();
                _logger.Log(LogLevel.Debug, $"Sitter service changed");
            }

            return result;
        }

        public async Task<bool> ChangeIsDeletedSitterWork(int sitterWorkId, bool isDeleted)
        {
            bool result =false;

            var sitterWork =await _workContext.SitterWorks
               .SingleOrDefaultAsync(sw => sw.Id == sitterWorkId && sw.IsDeleted != isDeleted);

            if (sitterWork == null)
            {
                _logger.Log(LogLevel.Error, $"The sitter service already has this status or not");
                throw new FileNotFoundException($"The sitter service already has this status or not");
            }
            else
            {
                sitterWork.IsDeleted = isDeleted;
                await _workContext.SaveChangesAsync();
                result = true;
                _logger.Log(LogLevel.Debug, $"Status deleted sitter service changed");
            }

            return result;
        }

        public async Task<SitterWorkEntity> GetInfoSitterWork(int sitterWorkId, bool? isDeleted=null)
        {
            var sitterWork = await _workContext.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.User)
                .Include(sw => sw.LocationsWork)
                .ThenInclude(sw => sw.Location)
                .Include(sw => sw.LocationsWork)
                .ThenInclude(sw => sw.TimingLocationWorks)
                .ThenInclude(sw => sw.DayOfWeek)
                .SingleOrDefaultAsync(sw => (sw.Id == sitterWorkId && sw.IsDeleted == isDeleted) 
                || (sw.Id == sitterWorkId && isDeleted == null));

            if (sitterWork == null)
            {
                _logger.Log(LogLevel.Warn, $"sitter services with this id {sitterWorkId} and status isDeleted {isDeleted} no");
                throw new FileNotFoundException($"sitter services with this id {sitterWorkId} and status isDeleted {isDeleted} no");
            }

            return sitterWork;
        }

        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id)
        {
            var sitterWorks = _workContext.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.User)
                .Include(sw => sw.LocationsWork)
                .ThenInclude(lw => lw.TimingLocationWorks)
                .ThenInclude(tlw => tlw.DayOfWeek)
                .Include(sw => sw.LocationsWork)
                .ThenInclude(tlw => tlw.Location)
                .Where(sw => sw.User.Id == id).ToList();

            if (sitterWorks.Count==0)
            {
                _logger.Log(LogLevel.Warn, $"sitter services with user id {id} no");
            }

            return sitterWorks;
        }

        //public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id,bool? workIsDeleted = null)
        //{
        //    if (!_workContext.Users.Any(u => u.Id == id))
        //    {
        //        throw FileNotFoundException
        //    }

        //    var sitterWorks = _workContext.SitterWorks
        //        .Include(sw => sw.WorkType)
        //        .Include(sw => sw.User)
        //        .Include(sw => sw.LocationsWork)
        //        .ThenInclude(lw => lw.TimingLocationWorks)
        //        .ThenInclude(tlw => tlw.DayOfWeek)
        //        .Include(sw => sw.LocationsWork)
        //        .ThenInclude(tlw => tlw.Location)
        //        .Where(sw => (sw.User.Id == id && workIsDeleted == null)
        //        ||(sw.User.Id == id && sw.IsDeleted == workIsDeleted)).ToList();

        //    if (sitterWorks.Count == 0)
        //    {
        //        _logger.Log(LogLevel.Warn, $"sitter services with user id {id} no");
        //    }

        //    return sitterWorks;
        //}

        public List<SitterWorkEntity> GetSitterWorksUser(int userId, bool? isDeleted = null)
        {
             if (!_workContext.Users.Any(u => u.Id == userId))
            {
                _logger.Log(LogLevel.Error, $"User by id not found");
                throw new FileNotFoundException("User by id not found");
            }

            var sitterWorks = _workContext.SitterWorks
                .Include(sw => sw.WorkType)
                .Include(sw => sw.User)
                .Include(sw => sw.LocationsWork)
                .Where(sw => (sw.UserId == userId && sw.IsDeleted == isDeleted)||
                (sw.UserId == userId && isDeleted == null)).ToList();

            if (sitterWorks.Count == 0)
            {
                _logger.Log(LogLevel.Warn, $"sitter services with user id {userId} and status isDeleted {isDeleted} no");
            }

            return sitterWorks;
        }

        public List<SitterWorkEntity> get()
        {
            var t = new List<SitterWorkEntity>();

            t = _workContext.SitterWorks.ToList();

            return t;

        }

        //public List<SitterWorkEntity> GetSitterWorks()
        //{
        //    var sitterWorks = _workContext.SitterWorks
        //            .Include(sw => sw.WorkType)
        //            .Include(sw => sw.User)
        //            .Include(sw => sw.LocationsWork).ToList();

        //    if (sitterWorks.Count == 0)
        //    {
        //        _logger.Log(LogLevel.Warn, $"No sitter service");
        //        //throw new FileNotFoundException($"No sitter service");
        //    }

        //    return sitterWorks;
        //}

        public async Task<List<SitterWorkEntity>> GetSitterWorks(bool? isDeleted = null)
        {
            var sitterWorks = await _workContext.SitterWorks
                    .Include(sw => sw.WorkType)
                    .Include(sw => sw.User)
                    .Include(sw => sw.LocationsWork)
                    .Where(sw => sw.IsDeleted == isDeleted || isDeleted == null).ToListAsync();

            if (sitterWorks.Count == 0)
            {
                _logger.Log(LogLevel.Warn, $"There are no sitter services or they do not correspond to the indicated status");
            }

            return sitterWorks;
        }

        public async Task<List<WorkTypeEntity>> GetAllWorkTypes(bool? isDeleted = null)
        {
            var workTypes = await _workContext.WorkTypes
                .Where(d => d.IsDeleted == isDeleted || isDeleted ==null).ToListAsync();

            if (workTypes.Count == 0)
            {
                _logger.Log(LogLevel.Warn, $"Work work no or not match status isDeleted {isDeleted}");
            }

            return workTypes;
        }
    }
}
