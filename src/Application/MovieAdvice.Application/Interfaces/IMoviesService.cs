using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Domain.ApiModels.MovieApi;
using MovieAdvice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Interfaces
{
    public interface IMoviesService
    {
        void SaveMovie(List<MovieApiModel> movieApiModelList);
        Task<GetMovieDto> GetMovie(int movieId);
        void UpdateMoviesStatus();
        Task<List<MovieListDto>> GetActiveMovies(int pageIndex);
        Task<int> GetActiveMoviesTotalPage();
        Task<bool> IsMovieExists(int movieId);
    }
}
