using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Dtos.Movie
{
    public class MovieListInfoDto
    {
        public List<MovieListDto> Movies { get; set; } = new();
        public int TotalPage { get; set; } = new();
        public int Page { get; set; }
    }
}
