using AutoMapper;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
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

        [HttpPost("newTimings/{locationWorkId}")]
        public async Task<ActionResult<List<TimingLocationWorkResponseDto>>> AddNewTimesWorks(int locationWorkId,List<TimingLocationWorkRequestDto> newTimings)
        {
            ChekWriteTime(newTimings);
            var newTimingsAdd = new List<TimingLocationWorkResponseDto>();

            try
            {
                var newTimingsBll = _mapper.Map<List<TimingLocationWorkRequest>>(newTimings);
                newTimingsAdd = _mapper.Map<List<TimingLocationWorkResponseDto>>(await _timeWorkService.AddNewTimeWork(locationWorkId, newTimingsBll));

                return Created(new Uri("api/Timings", UriKind.Relative), newTimingsAdd);
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
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TimingLocationWorkResponseDto>> AddNewTimeWork([FromQuery] TimingLocationWorkRequestDto newTiming)
        {
            CheckWriteTime(newTiming);
            TimingLocationWorkResponseDto newTimingAdd = null;

            try
            {
                var newTimingBll = _mapper.Map<TimingLocationWorkRequest>(newTiming);
                newTimingAdd = _mapper.Map<TimingLocationWorkResponseDto>(await _timeWorkService.AddNewTimeWork(newTimingBll));

                return Created(new Uri("api/Timing", UriKind.Relative), newTimingAdd);
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
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<TimingLocationWorkResponseDto>> UpdateTimeWork([FromQuery] TimingLocationWorkWithIdRequesDto updateTiming)
        {
            var chetTiming = _mapper.Map<TimingLocationWorkRequestDto>(updateTiming);
            CheckWriteTime(chetTiming);
            TimingLocationWorkResponseDto newTimingAdd = null;

            try
            {
                var newTimingBll = _mapper.Map<TimingLocationWorkWithIdRequest>(updateTiming);
                newTimingAdd = _mapper.Map<TimingLocationWorkResponseDto>(await _timeWorkService.UpdateTimeWork(newTimingBll));

                return Created(new Uri("api/Timing", UriKind.Relative), newTimingAdd);
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
                return NotFound(ex.Message);
            }
        }

        private void ChekWriteTime(List<TimingLocationWorkRequestDto> timings)
        {
            string exceptionMessage = "";

            foreach (var t in timings)
            {
                var validate = _validations.Validate(t);

                if (!validate.IsValid)
                {
                    foreach (var er in validate.Errors)
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
            var validate = _validations.Validate(timing);

            if (!validate.IsValid)
            {
                foreach (var er in validate.Errors)
                {
                    _logger.Log(LogLevel.Error, $"Property {er.PropertyName} failed validation. Error was: {er.AttemptedValue} {er.ErrorMessage}");
                    exceptionMessage = $"{er.AttemptedValue} {er.ErrorMessage}";
                }
                throw new InvalidWriteTimeException(exceptionMessage);
            }
        }


        [HttpDelete("{timingId}")]
        public async Task<ActionResult<bool>> DeleteTiming(int timingId)
        {
            try
            {
                bool isDeleted = await _timeWorkService.DeleteTiming(timingId);
                return Ok(isDeleted);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("timing/{timingId}")]
        public ActionResult<TimingLocationWorkResponseDto> GetOneTiming(int timingId)
        {
            try
            {
                var timing = _mapper.Map<TimingLocationWorkResponseDto>
                    (_timeWorkService.GetTiming(timingId));
                return Ok(timing);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("byLocationWork/{locationId}")]
        public async Task<ActionResult<List<TimingLocationWorkResponseDto>>> GetAllTimingOfLocationWork(int locationId)
        {
            try
            {
                var timings = _mapper.Map<List<TimingLocationWorkResponseDto>>
                    (await _timeWorkService.GetAllTimigsOfLocationWork(locationId));
                return Ok(timings);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Days")]
        public async Task<ActionResult<List<DayOfWeekResponseDto>>> GetDaysOfWeek()
        {
            var days = _mapper.Map<List<DayOfWeekResponseDto>>
                (await _timeWorkService.GetDaysOfWeek());
            return Ok(days);
        }
    }
}
