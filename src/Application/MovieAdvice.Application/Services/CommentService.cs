using AutoMapper;
using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Domain.Models;

namespace MovieAdvice.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;

        }
        public async Task<CommentSaveDto> SaveComment(CommentSaveDto commentSaveDto)
        {
            return mapper.Map<CommentSaveDto>(await commentRepository.SaveComment(mapper.Map<Comment>(commentSaveDto)));
        }
    }
}
