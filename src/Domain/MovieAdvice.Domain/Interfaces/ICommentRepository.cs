using MovieAdvice.Domain.Models;

namespace MovieAdvice.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> SaveComment(Comment comment);
    }
}
