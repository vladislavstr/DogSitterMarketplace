using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceBll.Services
{
    public class LocationService: ILocationService
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

        public bool AddNewLocationWork(LocationWorkRequest location)
        {
            bool result = false;

            if (location.Price <= 0)
            {
                _logger.Log(LogLevel.Error, $"service price cannot be less than 0");
                throw new ExcepsionOfWorkOnLocation($"service price cannot be less than 0 ");
            }

            var oldLocationWork = _workAndLocationRepo.GetAllLocationsWorkBySitterWork(location.SitterWorkId);
            var newLocationWornBll = _mapper.Map<LocationWorkEntity>(location);

            if (oldLocationWork != null)
            {
                if (oldLocationWork.Exists(lw => lw.LocationId == newLocationWornBll.LocationId && lw.IsNotActive == false))
                {
                    _logger.Log(LogLevel.Warn, $"The new location {newLocationWornBll.ToString()} is already in use for the specified job type");
                    throw new ExcepsionOfWorkOnLocation($"The new location {newLocationWornBll.ToString()} is already in use for the specified job type");
                }
                else
                {
                    result = _workAndLocationRepo.AddNewLocationWork(newLocationWornBll);
                    _logger.Log(LogLevel.Info, $"The new location is add");
                }
            }

            return result;
        }

        public bool UpdateLocationWork(UpdateLocationWorkRequest location)
        {
            bool result;

            if (location.Price <= 0)
            {
                _logger.Log(LogLevel.Error, $"service price cannot be less than 0");
                throw new ExcepsionOfWorkOnLocation($"service price cannot be less than 0 ");
            }

            var updateLocation = _mapper.Map<LocationWorkEntity>(location);
            var oldLocationWork = _workAndLocationRepo.GetAllLocationsWorkBySitterWork(updateLocation.SitterWorkId);

            if (oldLocationWork.Exists(lw => lw.LocationId == updateLocation.LocationId && lw.IsNotActive == false && lw.Id!=updateLocation.Id))
            {
                _logger.Log(LogLevel.Warn, $"The new location {updateLocation.ToString()} is already in use for the specified job type");
                throw new ExcepsionOfWorkOnLocation($"The new location {updateLocation.ToString()} is already in use for the specified job type");
            }
            else
            {
                result = _workAndLocationRepo.UpdateLocationWork(updateLocation);
                _logger.Log(LogLevel.Info, $"The new location is add");
            }

            return result;
        }

        public bool DeleteLocationWork(int locationWorkId)
        {
            return _workAndLocationRepo.DeleteLocationWork(locationWorkId);
        }

        public List<LocationWorkResponse> GetAllLocationWork()
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetAllLocationWork());
        }

        public LocationWorkResponse GetLocationWorkByid(int id)
        {
            return _mapper.Map<LocationWorkResponse>(_workAndLocationRepo.GetLocationWorkByid(id));
        }

        public List<LocationWorkResponse> GetAllLocationWorkbyActiveStatus(bool isNotActive = false)
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetAllLocationWorkbyActiveStatus(isNotActive));
        }

        public List<LocationWorkResponse> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false)
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetLocationsWorkBySitterWorkAndStatus(sitterWorkId, isNotActive));
        }

        public List<LocationWorkResponse> GetAllLocationWorkBySitterWork(int sitterWorkId)
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetAllLocationsWorkBySitterWork(sitterWorkId));
        }

        public List<LocationWorkResponse> GetAllLocationWorkByLocation(int locationId)
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetAllLocationWorkByLocation(locationId));
        }

        public List<LocationWorkResponse> GetAllLocationWorkByLocationAndStatus(int locationId, bool isNotActive = false)
        {
            return _mapper.Map<List<LocationWorkResponse>>(_workAndLocationRepo.GetAllLocationsWorkByLocationAndStatus(locationId, isNotActive));
        }
    }
}
