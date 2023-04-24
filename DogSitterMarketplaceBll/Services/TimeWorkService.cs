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
    public class TimeWorkService : ITimeWorkService
    {
        private readonly IMapper _mapper;
        private readonly ITimeWorkRepository _timeWorkRepository;
        private readonly IWorkAndLocationRepository _workAndLocationRepository;
        private readonly ILogger _logger;

        public TimeWorkService(ITimeWorkRepository timeRepo, IMapper map,
            IWorkAndLocationRepository workAndLocation, ILogger logger)
        {
            _mapper = map;
            _timeWorkRepository = timeRepo;
            _workAndLocationRepository = workAndLocation;
            _logger = logger;
        }


        public bool AddNewTimeWork(int locationWorkId, List<TimingLocationWorkRequest> timing)
        {
            bool result = false;

            if (timing == null)
            {
                throw new ArgumentNullException("Schedule list is empty.");
            }

            var locationWorkDal = _workAndLocationRepository.GetLocationWorkByid(locationWorkId);

            if (locationWorkDal == null)
            {
                throw new FileNotFoundException("There is no such location.");
            }

            var newTimingsLocationDal = _mapper.Map<List<TimingLocationWorkEntity>>(timing);
            CheckStartAndEndInterval(newTimingsLocationDal);
            var dublicatedTimings = GetDublicateInterval(newTimingsLocationDal, locationWorkDal.TimingLocationWorks);

            if (dublicatedTimings.Count != 0)
            {
                foreach (var d in dublicatedTimings)
                {
                    newTimingsLocationDal.Remove(d);
                    _logger.Log(LogLevel.Warn, $"This interval intersects with the existing {d.ToString()}");
                }
            }

            if (newTimingsLocationDal.Count != 0)
            {
                result = _timeWorkRepository.AddNewTimingsLocation(newTimingsLocationDal);
            }

            if (dublicatedTimings.Count != 0)
            {
                LogDublicateTiming(result, newTimingsLocationDal);
            }

            return result;
        }

        public bool AddNewTimeWork(TimingLocationWorkRequest timing)
        {
            bool result = false;

            if (timing == null)
            {
                _logger.Log(LogLevel.Error, $"empty interval passed");
                throw new ArgumentNullException("Schedule list is empty");
            }

            var locationWorkDal = _workAndLocationRepository.GetLocationWorkByid(timing.LocationWorkId);

            if (locationWorkDal == null)
            {
                _logger.Log(LogLevel.Error, $"There is no such location");
                throw new FileNotFoundException("There is no such location");
            }

            var newTimingLocationDal = _mapper.Map<TimingLocationWorkEntity>(timing);
            CheckStartAndEndInterval(newTimingLocationDal);
            var dublicatedTiming = GetDublicateInterval(newTimingLocationDal, locationWorkDal.TimingLocationWorks);

            if (dublicatedTiming != null)
            {
                _logger.Log(LogLevel.Warn, $"This interval intersects with the existing {dublicatedTiming.ToString()}");
                LogDublicateTiming();
            }

            result = _timeWorkRepository.AddNewTimingLocation(newTimingLocationDal);

            return result;
        }

        public bool UpdateTimeWork(TimingLocationWorkWithIdRequest timingUpdate)
        {
            bool isUpdate = false;

            if (timingUpdate == null)
            {
                _logger.Log(LogLevel.Error, $"empty interval passed");
                throw new ArgumentNullException("Schedule list is empty(");
            }

            var timingUpdateDal = _mapper.Map<TimingLocationWorkEntity>(timingUpdate);
            CheckStartAndEndInterval(timingUpdateDal);
            var oldTimings = _timeWorkRepository.GetAllTimigsOfLocationWork(timingUpdateDal.LocationWorkId);

            if (oldTimings == null)
            {
                _logger.Log(LogLevel.Error, $"There is no such location with id {timingUpdateDal.LocationWorkId}");
                throw new FileNotFoundException("There is no such location");
            }

            if (!oldTimings.Exists(t => t.Id == timingUpdateDal.Id))
            {
                _logger.Log(LogLevel.Error, $"Time interval with id {timingUpdateDal.Id} not found");
                throw new FileNotFoundException($"Time interval with id {timingUpdateDal.Id} not found");
            }
            else
            {
                oldTimings.RemoveAll(t => t.Id == timingUpdateDal.Id);
            }

            var dublicateTiming = GetDublicateInterval(timingUpdateDal, oldTimings);

            if (dublicateTiming != null)
            {
                _logger.Log(LogLevel.Warn, $"This interval intersects with the existing {dublicateTiming.ToString()}");
                LogDublicateTiming();
            }
            else
            {
                isUpdate = _timeWorkRepository.UpdateTimingLocation(timingUpdateDal);
            }

            return isUpdate;
        }

        private void CheckStartAndEndInterval(List<TimingLocationWorkEntity> timings)
        {
            List<TimingLocationWorkEntity> incorrectTimings = new List<TimingLocationWorkEntity>();
            incorrectTimings = timings.FindAll(t => t.Start > t.Stop);
            if (incorrectTimings.Count != 0)
            {
                string invalidValueTiming = "";
                foreach (var t in timings)
                {
                    _logger.Log(LogLevel.Error, $"The start of the interval is above its end {t.ToString()} ");
                    invalidValueTiming += t.ToString();
                }

                throw new InvalidWriteTimeException($"Beginnings of time intervals are greater than their ends {invalidValueTiming}");
            }
        }

        private void CheckStartAndEndInterval(TimingLocationWorkEntity timing)
        {
            if (timing.Start > timing.Stop)
            {
                _logger.Log(LogLevel.Error, $"The start of the interval is above its end {timing.ToString()}");
                throw new InvalidWriteTimeException($"Beginnings of time intervals are greater than their ends {timing.ToString()}");
            }
        }

        public bool DeleteTiming(int id)
        {
            return _timeWorkRepository.DeleteTiming(id);
        }

        private List<TimingLocationWorkEntity> GetDublicateInterval(List<TimingLocationWorkEntity> newTimings, List<TimingLocationWorkEntity> oldTimings)
        {
            List<TimingLocationWorkEntity> result = new List<TimingLocationWorkEntity>();

            foreach (var t in newTimings)
            {
                foreach (var ol in oldTimings)
                {
                    if (t.Stop > ol.Start && t.Start < ol.Stop && t.DayOfWeekId == ol.DayOfWeekId)
                    {
                        result.Add(t);
                    }
                }
            }

            return result;
        }

        private TimingLocationWorkEntity GetDublicateInterval(TimingLocationWorkEntity timing, List<TimingLocationWorkEntity> oldTimings)
        {
            TimingLocationWorkEntity result = null;

            foreach (var ol in oldTimings)
            {
                if (timing.Stop > ol.Start && timing.Start < ol.Stop && timing.DayOfWeekId == ol.DayOfWeekId)
                {
                    result = timing;
                }
            }

            return result;
        }

        private void LogDublicateTiming(bool newTimingAdd, List<TimingLocationWorkEntity> newTimings)
        {
            if (newTimingAdd is true)
            {
                string addTiming = "";
                foreach (var t in newTimings)
                {
                    addTiming += $"{t.ToString()} ";
                }
                throw new InvalidWriteTimeException($"one or more intervals intersect with existing ones, but this intervals are added {addTiming}");
            }
            else
            {
                _logger.Log(LogLevel.Error, $"one or more intervals intersect with existing ones, new intervals not Add ");
                throw new InvalidWriteTimeException("one or more intervals intersect with existing ones, new intervals not Add");
            }
        }

        private void LogDublicateTiming()
        {
            _logger.Log(LogLevel.Error, $"one or more intervals intersect with existing ones, new intervals not Add ");
            throw new InvalidWriteTimeException("one or more intervals intersect with existing ones, new intervals not Add");
        }

        public TimingLocationWorkResponse GetTiming(int id)
        {
            var result = _mapper.Map<TimingLocationWorkResponse>
                (_timeWorkRepository.GetTiming(id));

            return result;
        }

        public List<TimingLocationWorkResponse> GetAllTimigsOfLocationWork(int locationId)
        {
            var result = _mapper.Map<List<TimingLocationWorkResponse>>
                (_timeWorkRepository.GetAllTimigsOfLocationWork(locationId));

            return result;
        }
    }
}
