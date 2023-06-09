﻿using AutoMapper;
using DogSitterMarketplaceApi.Models.AppealsDto.Request;
using DogSitterMarketplaceApi.Models.AppealsDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using Microsoft.AspNetCore.Mvc;
using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppealController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppealService _appealService;

        public AppealController(IAppealService appealService, IMapper mapper, ILogger logger)
        {
            _logger = logger;
            _mapper = mapper;
            _appealService = appealService;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllAppeals")]
        public ActionResult<IEnumerable<AppealResponseDto>> GetAllAppeals()
        {
            try
            {
                return Ok(_appealService.GetAllAppeals());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("NotAnswered", Name = "GetAllNotAnsweredAppeals")]
        public ActionResult GetAllNotAnsweredAppeals()
        {
            try
            {
                return Ok(_appealService.GetAllNotAnsweredAppeals());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetAppealById")]
        public ActionResult GetAppealById(int id)
        {
            try
            {
                return Ok(_appealService.GetAppealById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Statuses", Name = "GetAllAppealStatuses")]
        public ActionResult<IEnumerable<AppealStatusResponseDto>> GetAllAppealStatuses()
        {
            try
            {
                return Ok(_appealService.GetAllAppealStatuses());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Types", Name = "GetAllAppealTypes")]
        public ActionResult<IEnumerable<AppealTypeResponseDto>> GetAllAppealTypes()
        {
            try
            {
                return Ok(_appealService.GetAllAppealTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ToWhom/{id:int}", Name = "GetAppealByUserIdToWhom")]
        public ActionResult GetAppealByUserIdToWhom(int id)
        {
            try
            {
                return Ok(_appealService.GetAppealByUserIdToWhom(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FromWhom/{id:int}", Name = "GetAppealByUserIdFromWhom")]
        public ActionResult GetAppealByUserIdFromWhom(int id)
        {
            try
            {
                return Ok(_appealService.GetAppealByUserIdFromWhom(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "AddAppeal")]
        public ActionResult<AppealResponseDto> AddAppeal(AppealRequestDto appeal)
        {
            try
            {
                var appealRequst = _mapper.Map<AppealRequest>(appeal);

                appealRequst.DateOfCreate = DateTime.UtcNow;
                appealRequst.StatusId = 1;

                var addAppealResponse = _appealService.AddAppeal(appealRequst);
                var addAppealResponseDto = _mapper.Map<AppealResponseDto>(addAppealResponse);

                return Created(new Uri("api/Appeal", UriKind.Relative), addAppealResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Status", Name = "AddAppealStatus")]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Type", Name = "AddAppealType")]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Status/{AppealId:int}", Name = "UpdateAppealStatusById")]
        public IActionResult UpdateAppealStatusById(int AppealId, int StatusId)
        {
            try
            {
                _appealService.UpdateAppealStatusById(AppealId, StatusId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("ResponseText/{id:int}", Name = "DoResponseTextById")]
        public ActionResult<AppealUpdateDto> DoResponseTextByAppealId(int id, string text, int statusId)
        {
            try
            {
                if (statusId != 1)
                {
                    if (text is not null)
                    {

                        var addAppealResponse = _appealService.DoResponseTextByAppeal(id, text, statusId);
                        var addAppealResponseDto = _mapper.Map<AppealResponseDto>(addAppealResponse);

                        return Created(new Uri("api/Appeal", UriKind.Relative), addAppealResponseDto);
                    }
                    else
                    {
                        throw new ArgumentException($"Appeal text of response must be filled in");
                    }
                }
                else
                {
                    throw new ArgumentException($"Appeal with stsrus id {statusId} can't exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

