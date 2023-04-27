using AutoMapper;
using DogSitterMarketplaceApi.Models.Works.Request;
using DogSitterMarketplaceApi.Models.WorksDto;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ILogger = NLog.ILogger;


namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LocationController(ILocationService locationService, IMapper mapper, ILogger logger)
        {
            _locationService = locationService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost("AddNewLocationWork", Name = "AddNewLocationWork")]
        public ActionResult<bool> AddNewLocationWork(LocationWorkRequestDto locationWork)
        {
            bool result = false;

            try
            {
                var newLocationWorkBll = _mapper.Map<LocationWorkRequest>(locationWork);
                result = _locationService.AddNewLocationWork(newLocationWorkBll);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepsionOfWorkOnLocation ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateLocationWork", Name = "UpdateLocationWork")]
        public ActionResult<bool> UpdateLocationWork(UpdateLocationWorkRequesDto locationWork)
        {
            bool result = false;

            try
            {
                var updateLocationWorkBll = _mapper.Map<UpdateLocationWorkRequest>(locationWork);
                result = _locationService.UpdateLocationWork(updateLocationWorkBll);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepsionOfWorkOnLocation ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLocationWork", Name = "DeleteLocationWork")]
        public ActionResult<bool> DeleteLocationWork([FromBody] int locationWorkId)
        {
            bool result;

            try
            {
                result = _locationService.DeleteLocationWork(locationWorkId);
                return Ok(result);
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

        [HttpGet("GetAllLocationWork", Name = "GetAllLocationWork")]
        public ActionResult<List<LocationWorkResponseDto>> GetAllLocationWork()
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(_locationService.GetAllLocationWork()));
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

        [HttpGet("GetAllLocationWorkbyActiveStatus", Name = "GetAllLocationWorkbyActiveStatus")]
        public ActionResult<List<LocationWorkResponseDto>> GetAllLocationWorkbyActiveStatus([FromQuery] bool isNotActive = false)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(_locationService.GetAllLocationWorkbyActiveStatus(isNotActive)));
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

        [HttpGet("GetLocationWorkByid/{id}", Name = "GetLocationWorkByid")]
        public ActionResult<LocationWorkResponseDto> GetLocationWorkByid(int id)
        {
            try
            {
                return Ok(_mapper.Map<LocationWorkResponseDto>(_locationService.GetLocationWorkByid(id)));
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

        [HttpGet("GetAllLocationWorkBySitterWork/{sitterWorkId}", Name = "GetAllLocationWorkBySitterWork")]
        public ActionResult<List<LocationWorkResponseDto>> GetAllLocationWorkBySitterWork(int sitterWorkId)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(_locationService.GetAllLocationWorkBySitterWork(sitterWorkId)));
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

        [HttpGet("GetLocationsWorkBySitterWorkAndStatus/{sitterWorkId}", Name = "GetLocationsWorkBySitterWorkAndStatus")]
        public ActionResult<List<LocationWorkResponseDto>> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, [FromQuery] bool isNotActive = false)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(_locationService.GetLocationsWorkBySitterWorkAndStatus(sitterWorkId, isNotActive)));
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

        [HttpGet("GetAllLocationWorkByLocation/{locationId}", Name = "GetAllLocationWorkByLocation")]
        public ActionResult<List<LocationWorkResponseDto>> GetAllLocationWorkByLocation(int locationId)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(_locationService.GetAllLocationWorkByLocation(locationId)));
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

        [HttpGet("GetAllLocationWorkByLocationAndStatus/{locationId}", Name = "GetAllLocationWorkByLocationAndStatus")]
        public ActionResult<List<LocationWorkResponseDto>> GetAllLocationWorkByLocationAndStatus(int locationId, [FromQuery] bool isNotActive = false)
        {
            try
            {
                return Ok(_mapper.Map<List<LocationWorkResponseDto>>(_locationService.GetAllLocationWorkByLocationAndStatus(locationId, isNotActive)));
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
