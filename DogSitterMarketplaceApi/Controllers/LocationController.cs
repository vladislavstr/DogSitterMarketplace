using AutoMapper;
using DogSitterMarketplaceApi.Models.Works.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;


namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController: Controller
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
    }
}
