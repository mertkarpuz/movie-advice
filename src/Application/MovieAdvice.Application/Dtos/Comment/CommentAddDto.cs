using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Dtos.Comment
{
    public class CommentAddDto
    {
        public int Point { get; set; }
        public string Description { get; set; } = "";
        public int MovieId { get; set; }
    }
}
