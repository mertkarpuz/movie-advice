using MovieAdvice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> SaveComment(Comment comment);
    }
}
