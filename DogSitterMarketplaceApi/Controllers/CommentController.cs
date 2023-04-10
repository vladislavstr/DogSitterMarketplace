using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        private readonly IMapper _mapper;

        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, IMapper mapper, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllComments")]
        public ActionResult<List<CommentOrderResponseDto>> GetAllComments()
        {
            try
            {
                var commentsResponse = _commentService.GetAllComments();
                var commentsResponseDto = _mapper.Map<List<CommentOrderResponseDto>>(commentsResponse);

                return Ok(commentsResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CommentController)} {nameof(GetAllComments)}");
                return Problem();
            }
        }

        [HttpGet("{id}", Name = "GetCommentById")]
        public ActionResult<CommentOrderResponseDto> GetCommentById(int id)
        {
            try
            {
                var commentResponse = _commentService.GetCommentById(id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CommentController), nameof(AddComment));
                return Problem();
            }
        }

        [HttpPut("{id}", Name = "UpdateComment")]
        public ActionResult<int> UpdateComment(CommentUpdateDto commentUpdatetDto)
        {
            try
            {
                var commentUpdate = _mapper.Map<CommentUpdate>(commentUpdatetDto);
                var id = _commentService.UpdateComment(commentUpdate);

                return Ok(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}