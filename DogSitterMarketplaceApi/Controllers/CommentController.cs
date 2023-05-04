using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Validations;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        private readonly ScoreValidator _scoreValidator;

        private readonly ScoreUpdateValidator _scoreUpdateValidator;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public CommentController(ICommentService commentService, IMapper mapper, ILogger nLogger)
        {
            _commentService = commentService;
            _scoreValidator = new ScoreValidator();
            _scoreUpdateValidator = new ScoreUpdateValidator();
            _mapper = mapper;
            _logger = nLogger;
        }

        [HttpGet("forClientAboutSitter/{userIdToComment}", Name = "GetCommentsAndScoresForClientAboutSitter")]
        [SwaggerOperation(Summary = "Get Comments And Scores For Client About Sitter")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<AvgScoreCommentsAboutOtherUsersResponseDto>> GetCommentsAndScoresForClientAboutSitter(int userIdGetComment, int userIdToComment)
        {
            try
            {
                var avgScoreCommentResponse = await _commentService.GetCommentsAndScoresAboutOtherUsers<CommentsAboutOtherUsersResponse>(userIdGetComment, UserRole.Client, userIdToComment, UserRole.Sitter);
                var result = _mapper.Map<AvgScoreCommentsAboutOtherUsersResponseDto>(avgScoreCommentResponse);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForClientAboutSitter)}");
                return BadRequest();
            }
        }

        [HttpGet("forSitterAboutClient/{userIdToComment}", Name = "GetCommentsAndScoresForSitterAboutClient")]
        [SwaggerOperation(Summary = "Get Comments And Scores For Sitter About Client")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]

        public async Task<ActionResult<AvgScoreCommentsAboutOtherUsersResponseDto>> GetCommentsAndScoresForSitterAboutClient(int userIdGetComment, int userIdToComment)
        {
            try
            {
                var avgScoreCommentsResponse = await _commentService.GetCommentsAndScoresAboutOtherUsers<CommentsAboutOtherUsersResponse>(userIdGetComment, UserRole.Sitter, userIdToComment, UserRole.Client);
                var result = _mapper.Map<AvgScoreCommentsAboutOtherUsersResponseDto>(avgScoreCommentsResponse);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForSitterAboutClient)}");
                return BadRequest();
            }
        }

        [HttpGet("forClientAboutHim/{userId}", Name = "GetCommentsAndScoresForClientAboutHim")]
        [SwaggerOperation(Summary = "Get Comments And Scores For Client About Him")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<AvgScoreCommentsResponseDto>> GetCommentsAndScoresForClientAboutHim(int userId)
        {
            try
            {
                var avgScoreCommentsResponse = await _commentService.GetCommentsAndScoresForUserAboutHim<CommentWithUserShortResponse>(userId, UserRole.Client);
                var result = _mapper.Map<AvgScoreCommentsResponseDto>(avgScoreCommentsResponse);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForClientAboutHim)}");
                return BadRequest();
            }
        }

        [HttpGet("forSitterAboutHim/{userId}", Name = "GetCommentsAndScoresForSitterAboutHim")]
        [SwaggerOperation(Summary = "Get Comments And Scores For Sitter About Him")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<AvgScoreCommentWithoutUserResponseDto>> GetCommentsAndScoresForSitterAboutHim(int userId)
        {
            try
            {
                var avgScoreCommentsResponse = await _commentService.GetCommentsAndScoresForUserAboutHim<CommentResponse>(userId, UserRole.Sitter);
                var result = _mapper.Map<AvgScoreCommentWithoutUserResponseDto>(avgScoreCommentsResponse);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForSitterAboutHim)}");
                return BadRequest();
            }
        }

        [HttpGet(Name = "GetAllNotDeletedComments")]
        [SwaggerOperation(Summary = "Get All Not Deleted Comments")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        public async Task<ActionResult<List<CommentOrderResponseDto>>> GetAllNotDeletedComments()
        {
            try
            {
                var commentsResponse = await _commentService.GetAllNotDeletedComments();
                var commentsResponseDto = _mapper.Map<List<CommentOrderResponseDto>>(commentsResponse);

                return Ok(commentsResponseDto);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetNotDeletedCommentById")]
        [SwaggerOperation(Summary = "Get Not Deleted Comment By Id")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<CommentOrderResponseDto>> GetCommentById(int id)
        {
            try
            {
                var commentResponse = await _commentService.GetNotDeletedCommentById(id);
                var commentResponseDto = _mapper.Map<CommentOrderResponseDto>(commentResponse);

                return Ok(commentResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentById)}");
                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteCommentById")]
        [SwaggerOperation(Summary = "Delete Comment By Id")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> DeleteCommentById(int id)
        {
            try
            {
                await _commentService.DeleteCommentById(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(DeleteCommentById)}");
                return BadRequest();
            }
        }

        [HttpPost(Name = "AddComment")]
        [SwaggerOperation(Summary = "Add Comment")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<CommentOrderResponseDto>> AddComment(CommentRequestDto addCommentRequestDto)
        {
            var validationResult = _scoreValidator.Validate(addCommentRequestDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var commentRequest = _mapper.Map<CommentRequest>(addCommentRequestDto);
                var addCommentResponse = await _commentService.AddComment(commentRequest);
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
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(AddComment)}");
                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdateComment")]
        [SwaggerOperation(Summary = "Update Comment")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<CommentOrderResponseDto>> UpdateComment(CommentUpdateDto commentUpdatetDto)
        {
            var validationResult = _scoreUpdateValidator.Validate(commentUpdatetDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var commentUpdate = _mapper.Map<CommentUpdate>(commentUpdatetDto);
                var updateCommentResponse = await _commentService.UpdateComment(commentUpdate);
                var commentResponseDto = _mapper.Map<CommentOrderResponseDto>(updateCommentResponse);

                return Ok(commentResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(UpdateComment)}");
                return BadRequest();
            }
        }
    }
}