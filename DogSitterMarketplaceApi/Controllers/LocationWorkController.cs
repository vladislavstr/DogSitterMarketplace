using AutoMapper;
using DogSitterMarketplaceApi.Models.Works.Request;
using DogSitterMarketplaceApi.Models.WorksDto;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
//using ILogger = NLog.ILogger;


namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationWorkController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        //private readonly ILogger _logger;

        public LocationWorkController(ILocationService locationService, IMapper mapper/*, ILogger logger*/)
        {
            _locationService = locationService;
            _mapper = mapper;
            //_logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<LocationWorkResponseDto>> AddNewLocationWork([FromQuery]LocationWorkRequestDto locationWork)
        {
            LocationWorkResponseDto result;

            try
            {
                var newLocationWorkBll = _mapper.Map<LocationWorkBaseRequest>(locationWork);
                result = _mapper.Map<LocationWorkResponseDto>(await _locationService.AddNewLocationWork(newLocationWorkBll));
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ExcepsionOfWorkOnLocation ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<LocationWorkResponseDto>> UpdateLocationWork([FromQuery]UpdateLocationWorkRequesDto locationWork)
        {
            LocationWorkResponseDto result;

            try
            {
                var updateLocationWorkBll = _mapper.Map<LocationWorkUpdateRequest>(locationWork);
                result = _mapper.Map<LocationWorkResponseDto>(await _locationService.UpdateLocationWork(updateLocationWorkBll));
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ExcepsionOfWorkOnLocation ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{locationWorkId}")]
        public async Task<ActionResult<bool>> DeleteLocationWork(int locationWorkId)
        {
            bool result;

            try
            {
                result = await _locationService.DeleteLocationWork(locationWorkId);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("all")]
        public ActionResult<List<LocationWorkBaseResponseDto>> GetAllLocationWork(bool? isNotActive)
        {
            try
            {
                 return Ok(_mapper.Map<List<LocationWorkBaseResponseDto>>(_locationService.GetAllLocationWork(isNotActive)));
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpGet("byActiveStatus/{isNotActive}")]
        //public async Task<ActionResult<List<LocationWorkResponseDto>>> GetAllLocationWorkbyActiveStatus(bool isNotActive = false)
        //{
        //    try
        //    {
        //        return Ok(_mapper.Map<List<LocationWorkResponseDto>>(await _locationService.GetAllLocationWorkbyActiveStatus(isNotActive)));
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("one/{id}")]
        public async Task<ActionResult<LocationWorkResponseDto>> GetLocationWorkByid(int id)
        {
            try
            {
                return Ok(_mapper.Map<LocationWorkResponseDto>(await _locationService.GetLocationWorkByid(id)));
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("bySytter/{sitterWorkId}")]
        public async Task<ActionResult<List<LocationWorkBaseResponseDto>>> GetAllLocationWorkBySitterWork(int sitterWorkId,[FromQuery]bool? isNotActive)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkBaseResponseDto>>(await _locationService.GetAllLocationWorkBySitterWork(sitterWorkId,isNotActive)));
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpGet("bySytterAndStatus/{sitterWorkId}_{isNotActive}")]
        //public async Task<ActionResult<List<LocationWorkResponseDto>>> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false)
        //{
        //    try
        //    {
        //        return Ok(_mapper.Map<List<LocationWorkResponseDto>>(await _locationService.GetLocationsWorkBySitterWorkAndStatus(sitterWorkId, isNotActive)));
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpGet("byLocation/{locationId}")]
        public async Task<ActionResult<List<LocationWorkResponseDto>>> GetAllLocationWorkByLocation(int locationId, [FromQuery] bool? isNotActive)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(await _locationService.GetAllLocationWorkByLocation(locationId, isNotActive)));
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("byLocationAndStatus/{locationId}_{isNotActive}")]
        //public async Task<ActionResult<List<LocationWorkResponseDto>>> GetAllLocationWorkByLocationAndStatus(int locationId,bool isNotActive = false)
        //{
        //    try
        //    {
        //        return Ok(_mapper.Map<List<LocationWorkResponseDto>>(await _locationService.GetAllLocationWorkByLocationAndStatus(locationId, isNotActive)));
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpGet("locations")]
        public async Task<ActionResult<List<LocationResponseDto>>> GetAllLocation( bool? isDeleted)
        {
            return _mapper.Map<List<LocationResponseDto>>(await _locationService.GetAllLocation(isDeleted));
        }
    }
}
