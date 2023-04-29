using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Validations;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceCore.Exceptions;
using FluentValidation;
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
        public ActionResult<AvgScoreCommentsAboutOtherUsersResponseDto> GetCommentsAndScoresForClientAboutSitter(int userIdGetComment, int userIdToComment)
        {
            try
            {
                var avgScoreCommentResponse = _commentService.GetCommentsAndScoresAboutOtherUsers<CommentsAboutOtherUsersResponse>(userIdGetComment, UserRole.Client, userIdToComment, UserRole.Sitter);
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
                // _logger.LogError($"{nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForClientAboutSitter)}");
                return BadRequest();
            }
        }

        [HttpGet("forSitterAboutClient/{userIdToComment}", Name = "GetCommentsAndScoresForSitterAboutClient")]
        public ActionResult<AvgScoreCommentsAboutOtherUsersResponseDto> GetCommentsAndScoresForSitterAboutClient(int userIdGetComment, int userIdToComment)
        {
            try
            {
                var avgScoreCommentsResponse = _commentService.GetCommentsAndScoresAboutOtherUsers<CommentsAboutOtherUsersResponse>(userIdGetComment, UserRole.Sitter, userIdToComment, UserRole.Client);
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
                // _logger.LogError($"{nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForSitterAboutClient)}");
                return BadRequest();
            }
        }

        [HttpGet("forClientAboutHim/{userId}", Name = "GetCommentsAndScoresForClientAboutHim")]
        public ActionResult<AvgScoreCommentsResponseDto> GetCommentsAndScoresForClientAboutHim(int userId)
        {
            try
            {
                var avgScoreCommentsResponse = _commentService.GetCommentsAndScoresForUserAboutHim<CommentWithUserShortResponse>(userId, UserRole.Client);
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
                // _logger.LogError($"{nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForClientAboutHim)}");
                return BadRequest();
            }
        }

        [HttpGet("forSitterAboutHim/{userId}", Name = "GetCommentsAndScoresForSitterAboutHim")]
        public ActionResult<AvgScoreCommentWithoutUserResponseDto> GetCommentsAndScoresForSitterAboutHim(int userId)
        {
            try
            {
                var avgScoreCommentsResponse = _commentService.GetCommentsAndScoresForUserAboutHim<CommentResponse>(userId, UserRole.Sitter);
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
                // _logger.LogError($"{nameof(CommentController)} {nameof(GetAllNotDeletedComments)}");
                _logger.Log(NLog.LogLevel.Error, $"{ex} {nameof(CommentController)} {nameof(GetCommentsAndScoresForSitterAboutHim)}");
                return BadRequest();
            }
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
            var validationResult = _scoreValidator.Validate(addCommentRequestDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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
            var validationResult = _scoreUpdateValidator.Validate(commentUpdatetDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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