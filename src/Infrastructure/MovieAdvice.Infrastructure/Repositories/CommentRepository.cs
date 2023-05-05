using MovieAdvice.Domain.Models;
using MovieAdvice.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Infrastructure.Repositories
{
    public class CommentRepository
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
