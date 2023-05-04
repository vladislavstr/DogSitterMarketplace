using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Works;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceBll.Services
{
    public class LocationService : ILocationService
    {
        private static IWorkAndLocationRepository _workAndLocationRepo;
        private static IMapper _mapper;
        private static ILogger _logger;

        public LocationService(IWorkAndLocationRepository workAndLocationRepo
            , IMapper mapper, ILogger logger)
        {
            _workAndLocationRepo = workAndLocationRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LocationWorkResponse> AddNewLocationWork(LocationWorkBaseRequest location)
        {
            LocationWorkResponse result = null;

            if (location.Price <= 0)
            {
                _logger.Log(LogLevel.Error, $"service price cannot be less than 0");
                throw new ExcepsionOfWorkOnLocation($"service price cannot be less than 0 ");
            }

            var oldLocationWork = await _workAndLocationRepo.GetAllLocationsWorkBySitterWork(location.SitterWorkId);
            var newLocationWornBll = _mapper.Map<LocationWorkEntity>(location);

            if (oldLocationWork.Count != 0)
            {
                if (oldLocationWork.Exists(lw => lw.LocationId == newLocationWornBll.LocationId && lw.IsNotActive == false))
                {
                    _logger.Log(LogLevel.Warn, $"The new location {newLocationWornBll.ToString()} is already in use for the specified job type");
                    throw new ExcepsionOfWorkOnLocation($"The new location {newLocationWornBll.ToString()} is already in use for the specified job type");
                }
            }

            result = _mapper.Map<LocationWorkResponse>(await _workAndLocationRepo.AddNewLocationWork(newLocationWornBll));

            return result;
        }

        public async Task<LocationWorkResponse> UpdateLocationWork(LocationWorkUpdateRequest location)
        {
            LocationWorkResponse result;

            if (location.Price <= 0)
            {
                _logger.Log(LogLevel.Error, $"service price cannot be less than 0");
                throw new ExcepsionOfWorkOnLocation($"service price cannot be less than 0 ");
            }

            var updateLocation = _mapper.Map<LocationWorkEntity>(location);
            var oldLocationWork = await _workAndLocationRepo.GetAllLocationsWorkBySitterWork(updateLocation.SitterWorkId);

            if (!oldLocationWork.Exists(lw => lw.Id == updateLocation.Id))
            {
                _logger.Log(LogLevel.Error, $"There is no location for the service with this ID {updateLocation.Id}");
                throw new FileNotFoundException($"There is no location for the service with this ID {updateLocation.Id}");
            }
            else if (oldLocationWork.Exists(lw => lw.LocationId == updateLocation.LocationId && lw.IsNotActive == false && lw.Id != updateLocation.Id))
            {
                _logger.Log(LogLevel.Error, $"The new location {updateLocation.ToString()} is already in use for the specified job type");
                throw new ExcepsionOfWorkOnLocation($"The new location {updateLocation.ToString()} is already in use for the specified job type");
            }
            else
            {
                result = _mapper.Map<LocationWorkResponse>(await _workAndLocationRepo.UpdateLocationWork(updateLocation));
                _logger.Log(LogLevel.Info, $"The new location is add");
            }

            return result;
        }

        public async Task<bool> DeleteLocationWork(int locationWorkId)
        {
            return await _workAndLocationRepo.DeleteLocationWork(locationWorkId);
        }

        public List<LocationWorkResponse> GetAllLocationWork(bool? isNotActive)
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetAllLocationWork(isNotActive));
        }

        public async Task<LocationWorkResponse> GetLocationWorkByid(int id)
        {
            return _mapper.Map<LocationWorkResponse>(await _workAndLocationRepo.GetLocationWorkByid(id));
        }

        //public async Task<List<LocationWorkResponse>> GetAllLocationWorkbyActiveStatus(bool isNotActive = false)
        //{
        //    return _mapper.Map<List<LocationWorkResponse>>(await _workAndLocationRepo.GetAllLocationWorkbyActiveStatus(isNotActive));
        //}

        //public async Task<List<LocationWorkResponse>> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false)
        //{
        //    return _mapper.Map<List<LocationWorkResponse>>(await _workAndLocationRepo.GetLocationsWorkBySitterWorkAndStatus(sitterWorkId, isNotActive));
        //}

        public async Task<List<LocationWorkResponse>> GetAllLocationWorkBySitterWork(int sitterWorkId, bool? isNotActive)
        {
            return _mapper.Map<List<LocationWorkResponse>>(await _workAndLocationRepo.GetAllLocationsWorkBySitterWork(sitterWorkId, isNotActive));
        }

        public async Task<List<LocationWorkResponse>> GetAllLocationWorkByLocation(int locationId, bool? isNotActive)
        {
            return _mapper.Map<List<LocationWorkResponse>>(await _workAndLocationRepo.GetAllLocationWorkByLocation(locationId, isNotActive));
        }

        //public async Task<List<LocationWorkResponse>> GetAllLocationWorkByLocationAndStatus(int locationId, bool isNotActive = false)
        //{
        //    return _mapper.Map<List<LocationWorkResponse>>(await _workAndLocationRepo.GetAllLocationsWorkByLocationAndStatus(locationId, isNotActive));
        //}

        //public async Task<List<LocationResponse>> GetAllLocation()
        //{
        //    return _mapper.Map<List<LocationResponse>>(await _workAndLocationRepo.GetAllLocation());
        //}

        public async Task<List<LocationResponse>> GetAllLocation(bool? IsDeleted)
        {
            return _mapper.Map<List<LocationResponse>>(await _workAndLocationRepo.GetAllLocation(IsDeleted));
        }
    }
}
