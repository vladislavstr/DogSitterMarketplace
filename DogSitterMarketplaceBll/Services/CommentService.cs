﻿using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceDal.IRepositories;

namespace DogSitterMarketplaceBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public List<CommentResponse> GetAllComments()
        {
            var commentsEntity = _commentRepository.GetAllComments();
            var commentsResponse = _mapper.Map<List<CommentResponse>>(commentsEntity);

            return commentsResponse;
        }
    }
}
