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
                //var allAppeals = _appealService.GetAllAppeals();
                //var allAppealsDto = _mapper.Map<IEnumerable<AppealResponseDto>>(allAppeals);
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

        [HttpGet("{id}", Name = "GetAppealById")]
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

        [HttpDelete("{id}", Name = "DeleteAppealById")]
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

        [HttpPost("AddAppeal", Name = "AddAppeal")]
        public ActionResult<AppealResponseDto> AddAppeal(AppealRequestDto user)
        {
            try
            {
                var appealRequst = _mapper.Map<AppealRequest>(user);
                var addAppealResponse = _appealService.AddAppeal(appealRequst);
                var addAppealResponseDto = _mapper.Map<AppealResponseDto>(addAppealResponse);

                return Created(new Uri("api/Appeal", UriKind.Relative), addAppealResponseDto);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
