using MovieAdvice.Application.Dtos.Comment;


namespace MovieAdvice.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentSaveDto> SaveComment(CommentSaveDto commentSaveDto);
    }
}
