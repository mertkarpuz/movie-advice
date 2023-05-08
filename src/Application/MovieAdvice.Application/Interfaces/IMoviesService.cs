using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Domain.ApiModels.MovieApi;


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
