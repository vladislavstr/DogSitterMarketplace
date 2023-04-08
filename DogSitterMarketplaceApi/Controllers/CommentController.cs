using AutoMapper;
using DogSitterMarketplaceBll.IServices;

namespace DogSitterMarketplaceApi.Controllers
{
    public class CommentController
    {
        private readonly ICommentService _commentService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public CommentController(ICommentService commentService, IMapper mapper, ILogger logger)
        {
            _commentService = commentService;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
