using MovieAdvice.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Interfaces
{
    public interface IGetMoviesService
    {
        Task<RootApiModel?> GetMovies(int page);
    }
}
