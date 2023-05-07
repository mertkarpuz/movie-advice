using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentSaveDto> SaveComment(CommentSaveDto commentSaveDto);
    }
}
