using AutoMapper;
using DogSitterMarketplaceApi.Models.WorksDto;
using DogSitterMarketplaceApi.Models.WorksDto.Request;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using Microsoft.AspNetCore.Mvc;
//using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitterWorkController : Controller
    {
        private readonly IWorkService _workService;
        private readonly IMapper _mapper;
        //private readonly ILogger _logger;

        public SitterWorkController(IWorkService workService, IMapper mapper/*, ILogger logger*/)
        {
            _workService = workService;
            _mapper = mapper;
            //_logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<SitterWorkBaseResponseDto>> AddSitterWork([FromQuery] SitterWorkBaseRequestDto sitterWork)
        {
            try
            {
                var sitterWorkBll = _mapper.Map<SitterWorkBaseRequest>(sitterWork);
                var result = _mapper.Map<SitterWorkBaseResponseDto>(await _workService.AddSitterWork(sitterWorkBll));
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<SitterWorkResponseDto>> UpdateSitterWork([FromQuery] SitterWorkUpdateRequestDto sitterWork)
        {
            try
            {
                var sitterWorkBll = _mapper.Map<SitterWorkRequest>(sitterWork);
                var result = _mapper.Map<SitterWorkResponseDto>(await _workService.UpdateSitterWork(sitterWorkBll));
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{sitterWorkId}")]
        public async Task<ActionResult<bool>> ChangeIsDeletedSitterWork(int sitterWorkId, [FromQuery] bool isDeleted)
        {
            bool result;
            try
            {
                result = await _workService.ChangeIsDeletedSitterWork(sitterWorkId, isDeleted);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("full/{sitterWorkId}")]
        public async Task<ActionResult<SitterWorkResponseDto>> GetInfoSitterWork(int sitterWorkId, [FromQuery] bool? isDeleted)
        {
            try
            {
                return Ok(_mapper.Map<SitterWorkResponseDto>(await _workService.GetInfoSitterWork(sitterWorkId, isDeleted)));
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byUser/{userId}")]
        public ActionResult<List<SitterWorkResponseDto>> GetSitterWorksUser(int userId, [FromQuery] bool? isDeleted = null)
        {
            try
            {
                return Ok(_mapper.Map <List<SitterWorkResponseDto>>(_workService.GetSitterWorksUser(userId, isDeleted)));
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<SitterWorkBaseResponseDto>>> GetSitterWorks([FromQuery] bool? IsDeleted = null)
        {
            return Ok(_mapper.Map<List<SitterWorkBaseResponseDto>>(await _workService.GetSitterWorks(IsDeleted)));
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<WorkTypeResponseDto>>> GetWorkTypes([FromQuery] bool? IsDeleted = null)
        {
            return Ok(_mapper.Map<List<WorkTypeResponseDto>>(await _workService.GetWorkTypes(IsDeleted)));
        }
    }
}
