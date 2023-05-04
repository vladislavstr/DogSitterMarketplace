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

        public async Task<List<TimingLocationWorkResponse>> AddNewTimeWork(int locationWorkId, List<TimingLocationWorkRequest> timing)
        {
            var result = new List<TimingLocationWorkResponse>();
            var locationWorkDal = await _workAndLocationRepository.GetLocationWorkByid(locationWorkId);

            if (locationWorkDal == null)
            {
                _logger.Log(LogLevel.Error, $"There is no such location work");
                throw new FileNotFoundException("There is no such location work");
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
                result = _mapper.Map<List<TimingLocationWorkResponse>>(await _timeWorkRepository.AddNewTimingsLocation(newTimingsLocationDal));
            }

            if (dublicatedTimings.Count != 0)
            {
                if (newTimingsLocationDal.Count != 0)
                {
                    string addTiming = "";
                    foreach (var t in newTimingsLocationDal)
                    {
                        addTiming += $"{t.ToString()} ";
                    }
                    _logger.Log(LogLevel.Warn, $"one or more intervals intersect with existing ones, but this intervals are added {addTiming}");
                    throw new InvalidWriteTimeException($"one or more intervals intersect with existing ones, but this intervals are added {addTiming}");
                }
                else
                {
                    _logger.Log(LogLevel.Error, $"one or more intervals intersect with existing ones, new intervals not Add ");
                    throw new InvalidWriteTimeException("one or more intervals intersect with existing ones, new intervals not Add");
                }
            }

            return result;
        }

        public async Task<TimingLocationWorkResponse> AddNewTimeWork(TimingLocationWorkRequest timing)
        {
            var locationWorkDal = await _workAndLocationRepository.GetLocationWorkByid(timing.LocationWorkId);

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
                _logger.Log(LogLevel.Error, $"one or more intervals intersect with existing ones, new intervals not Add ");
                throw new InvalidWriteTimeException("one or more intervals intersect with existing ones, new intervals not Add");
            }

            var result = _mapper.Map<TimingLocationWorkResponse>(await _timeWorkRepository.AddNewTimingLocation(newTimingLocationDal));

            return result;
        }

        public async Task<TimingLocationWorkResponse> UpdateTimeWork(TimingLocationWorkWithIdRequest timingUpdate)
        {
            TimingLocationWorkResponse result = null;
            var timingUpdateDal = _mapper.Map<TimingLocationWorkEntity>(timingUpdate);
            CheckStartAndEndInterval(timingUpdateDal);
            var oldTimings = await _timeWorkRepository.GetAllTimigsOfLocationWork(timingUpdateDal.LocationWorkId);

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
                _logger.Log(LogLevel.Error, $"This interval intersects with the existing {dublicateTiming.ToString()}");
                throw new InvalidWriteTimeException("one or more intervals intersect with existing ones, new intervals not Add");
            }
            else
            {
                result = _mapper.Map<TimingLocationWorkResponse>(await _timeWorkRepository.UpdateTimingLocation(timingUpdateDal));
            }

            return result;
        }

        public async Task<bool> DeleteTiming(int id)
        {
            return await _timeWorkRepository.DeleteTiming(id);
        }

        private void CheckStartAndEndInterval(List<TimingLocationWorkEntity> timings)
        {
            List<TimingLocationWorkEntity> incorrectTimings = new List<TimingLocationWorkEntity>();
            incorrectTimings = timings.FindAll(t => t.Start > t.Stop);
            if (incorrectTimings.Count != 0)
            {
                string invalidValueTiming = "";
                foreach (var t in incorrectTimings)
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

        public TimingLocationWorkResponse GetTiming(int id)
        {
            var result = _mapper.Map<TimingLocationWorkResponse>
                (_timeWorkRepository.GetTiming(id));

            return result;
        }

        public async Task<List<TimingLocationWorkResponse>> GetAllTimigsOfLocationWork(int locationId)
        {
            var result = _mapper.Map<List<TimingLocationWorkResponse>>
                (await _timeWorkRepository.GetAllTimigsOfLocationWork(locationId));

            return result;
        }

        public async Task<List<DayOfWeekResponse>> GetDaysOfWeek()
        {
            return _mapper.Map<List<DayOfWeekResponse>>(await _timeWorkRepository.GetDaysOfWeek());
        }
    }
}
