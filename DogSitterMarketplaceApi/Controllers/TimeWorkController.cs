using AutoMapper;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeWorkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITimeWorkService _timeWorkService;
        private readonly ILogger _logger;
        private readonly TimingLocationWorkRequestDtoValidator _validations;

        public TimeWorkController(IMapper map, ITimeWorkService timeWork, ILogger logger)
        {
            _mapper = map;
            _timeWorkService = timeWork;
            _logger = logger;
            _validations = new TimingLocationWorkRequestDtoValidator();
        }

        [HttpPost("addListTimingsToLocationWork", Name = "AddNewTimings")]
        public ActionResult<bool> AddNewTimesWorks(int locationWorkId, List<TimingLocationWorkRequestDto> newTimings)
        {
            ChekWriteTime(newTimings);
            bool newTimingsAdd = false;

            try
            {
                var newTimingsBll = _mapper.Map<List<TimingLocationWorkRequest>>(newTimings);
                newTimingsAdd = _timeWorkService.AddNewTimeWork(locationWorkId, newTimingsBll);

                return Ok(newTimingsAdd);
            }
            catch (InvalidWriteTimeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addOneTimingToLocationWork", Name = "AddNewTiming")]
        public ActionResult<bool> AddNewTimeWork(TimingLocationWorkRequestDto newTiming)
        {
            CheckWriteTime(newTiming);
            bool newTimingAdd = false;

            try
            {
                var newTimingBll = _mapper.Map<TimingLocationWorkRequest>(newTiming);
                newTimingAdd = _timeWorkService.AddNewTimeWork(newTimingBll);

                return Ok(newTimingAdd);
            }
            catch (InvalidWriteTimeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void ChekWriteTime(List<TimingLocationWorkRequestDto> timings)
        {
            string exceptionMessage = "";

            foreach (var t in timings)
            {
                var v = _validations.Validate(t);

                if (!v.IsValid)
                {
                    foreach (var er in v.Errors)
                    {
                        _logger.Log(LogLevel.Error, $"Property {er.PropertyName} failed validation. Error was: {er.AttemptedValue} {er.ErrorMessage}");
                        exceptionMessage = $"{er.AttemptedValue} {er.ErrorMessage}";
                    }
                }
            }

            if (exceptionMessage != "")
            {
                throw new InvalidWriteTimeException(exceptionMessage);
            }
        }

        private void CheckWriteTime(TimingLocationWorkRequestDto timing)
        {
            string exceptionMessage = "";
            var v = _validations.Validate(timing);

            if (!v.IsValid)
            {
                foreach (var er in v.Errors)
                {
                    _logger.Log(LogLevel.Error, $"Property {er.PropertyName} failed validation. Error was: {er.AttemptedValue} {er.ErrorMessage}");
                    exceptionMessage = $"{er.AttemptedValue} {er.ErrorMessage}";
                }
                throw new InvalidWriteTimeException(exceptionMessage);
            }
        }

        [HttpPut("UpdateOneTiming", Name = "GetComment")]
        public ActionResult<bool> UpdateTimeWork(TimingLocationWorkWithIdRequesDto timingUpdate)
        {
            var checkTiming = _mapper.Map<TimingLocationWorkRequestDto>(timingUpdate);
            CheckWriteTime(checkTiming);
            bool isUpdate = false;

            try
            {
                var timingUpdateBll = _mapper.Map<TimingLocationWorkWithIdRequest>(timingUpdate);
                isUpdate = _timeWorkService.UpdateTimeWork(timingUpdateBll);
                return Ok(isUpdate);
            }
            catch (InvalidWriteTimeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
