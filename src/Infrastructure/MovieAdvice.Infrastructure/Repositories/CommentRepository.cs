using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Domain.Models;
using MovieAdvice.Infrastructure.Context;


namespace MovieAdvice.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MovieAdviceDbContext context;
        public CommentRepository(MovieAdviceDbContext context)
        {
            this.context = context; 
        }

        public async Task<Comment> SaveComment(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
            return comment;
        }
    }
}
