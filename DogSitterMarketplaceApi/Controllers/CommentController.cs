using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public CommentController(ICommentService commentService, IMapper mapper, ILogger nLogger)
        {
            _commentService = commentService;
            _mapper = mapper;
            _logger = nLogger;
        }

        [HttpGet(Name = "GetAllNotDeletedComments")]
        public ActionResult<List<CommentOrderResponseDto>> GetAllNotDeletedComments()
        {
            try
            {
                var commentsResponse = _commentService.GetAllNotDeletedComments();
                var commentsResponseDto = _mapper.Map<List<CommentOrderResponseDto>>(commentsResponse);

                return Ok(commentsResponseDto);
            }
            catch (Exception ex)
            {
                // _logger.LogError($"{nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                return Problem();
            }
        }

        [HttpGet("{id}", Name = "GetNotDeletedCommentById")]
        public ActionResult<CommentOrderResponseDto> GetCommentById(int id)
        {
            try
            {
                var commentResponse = _commentService.GetNotDeletedCommentById(id);
                var commentResponseDto = _mapper.Map<CommentOrderResponseDto>(commentResponse);

                return Ok(commentResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}", Name = "DeleteCommentById")]
        public IActionResult DeleteCommentById(int id)
        {
            try
            {
                _commentService.DeleteCommentById(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "AddComment")]
        public ActionResult<CommentOrderResponseDto> AddComment(CommentRequestDto addCommentRequestDto)
        {
            try
            {
                var commentRequest = _mapper.Map<CommentRequest>(addCommentRequestDto);
                var addCommentResponse = _commentService.AddComment(commentRequest);
                var addCommentResponseDto = _mapper.Map<CommentOrderResponseDto>(addCommentResponse);

                return Created(new Uri("api/Comment", UriKind.Relative), addCommentResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                 return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdateComment")]
        public ActionResult<CommentOrderResponseDto> UpdateComment(CommentUpdateDto commentUpdatetDto)
        {
            try
            {
                var commentUpdate = _mapper.Map<CommentUpdate>(commentUpdatetDto);
                var updateCommentResponse = _commentService.UpdateComment(commentUpdate);
                var commentResponseDto = _mapper.Map<CommentOrderResponseDto>(updateCommentResponse);

                return Ok(commentResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}