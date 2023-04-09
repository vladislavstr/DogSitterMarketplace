using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceBll.IServices;
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

        //[HttpGet(Name = "GetAllComments")]
        //public ActionResult<CommentResponseDto> GetAllComments()
        //{
        //    try
        //    {
        //        var commentsResponse = _commentService.GetAllComments();
        //        var commentsResponseDto = _mapper.Map<List<CommentResponseDto>>(commentsResponse);

        //        return Ok(commentsResponseDto);
        //    }
        //    catch ()
        //    { 
        //    // прокинуть ошибки
        //    }
        //}
    }
}
