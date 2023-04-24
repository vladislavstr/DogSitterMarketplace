using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceApi.Models.AppealsDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceApi.Models.AppealsDto.Request;

namespace DogSitterMarketplaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppealController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppealService _appealService;

        public AppealController(IAppealService appealService, IMapper mapper)//, ILogger logger)
        {
            //_logger = logger;
            _mapper = mapper;
            _appealService = appealService;
        }

        [HttpGet("GrtPing")]
        public IActionResult GrtPing()
        {
            return Ok();
        }

        [HttpGet("GetAllAppeals", Name = "GetAllAppeals")]
        public ActionResult<IEnumerable<AppealResponseDto>> GetAllAppeals()
        {
            try
            {
                return Ok(_appealService.GetAllAppeals());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetAllNotDeletedAppeals", Name = "GetAllNotDeletedAppeals")]
        public ActionResult GetAllNotDeletedAppeals()
        {
            try
            {
                return Ok(_appealService.GetAllNotDeletedAppeals());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetAppealById/{id}", Name = "GetAppealById")]
        public ActionResult GetAppealById(int id)
        {
            try
            {
                return Ok(_appealService.GetAppealById(id));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetAppealByUserIdToWhom/{id}", Name = "GetAppealByUserIdToWhom")]
        public ActionResult GetAppealByUserIdToWhom(int id)
        {
            try
            {
                return Ok(_appealService.GetAppealByUserIdToWhom(id));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetAppealByUserIdFromWhom/{id}", Name = "GetAppealByUserIdFromWhom")]
        public ActionResult GetAppealByUserIdFromWhom(int id)
        {
            try
            {
                return Ok(_appealService.GetAppealByUserIdFromWhom(id));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        [HttpPost("AddAppeal", Name = "AddAppeal")]
        public ActionResult<AppealResponseDto> AddAppeal(AppealRequestDto appeal)
        {
            try
            {
                appeal.ResponseText = null;
                appeal.DateOfCreate = DateTime.Now;
                appeal.DateOfResponse = null;
                var appealRequst = _mapper.Map<AppealRequest>(appeal);
                var addAppealResponse = _appealService.AddAppeal(appealRequst);
                var addAppealResponseDto = _mapper.Map<AppealResponseDto>(addAppealResponse);

                return Created(new Uri("api/Appeal", UriKind.Relative), addAppealResponseDto);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("AddAppealStatus", Name = "AddAppealStatus")]
        public ActionResult<AppealStatusResponseDto> AddAppealStatus(AppealStatusRequestDto appealStatus)
        {
            try
            {
                var appealStatusRequst = _mapper.Map<AppealStatusRequest>(appealStatus);
                var addAppealStatusResponse = _appealService.AddAppealStatus(appealStatusRequst);
                var addAppealStatusResponseDto = _mapper.Map<AppealStatusResponseDto>(addAppealStatusResponse);

                return Created(new Uri("api/AppealStatus", UriKind.Relative), addAppealStatusResponseDto);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("AddAppealType", Name = "AddAppealType")]
        public ActionResult<AppealTypeResponseDto> AddAppealType(AppealTypeRequestDto appealType)
        {
            try
            {
                var appealTypeRequst = _mapper.Map<AppealTypeRequest>(appealType);
                var addAppealTypeResponse = _appealService.AddAppealType(appealTypeRequst);
                var addAppealTypeResponseDto = _mapper.Map<AppealTypeResponseDto>(addAppealTypeResponse);

                return Created(new Uri("api/AppealType", UriKind.Relative), addAppealTypeResponseDto);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        [HttpDelete("DeleteAppealById/{id}", Name = "DeleteAppealById")]
        public IActionResult DeleteAppealById(int id)
        {
            try
            {
                _appealService.DeleteAppealById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPut("UpdateAppealStatusById/{AppealId}", Name = "UpdateAppealStatusById")]
        public IActionResult UpdateAppealStatusById(int AppealId, int StatusId)
        {
            try
            {
                _appealService.UpdateAppealStatusById(AppealId, StatusId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
