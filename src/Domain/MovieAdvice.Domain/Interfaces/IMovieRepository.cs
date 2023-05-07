using MovieAdvice.Domain.Models;


namespace MovieAdvice.Domain.Interfaces
{
    public interface IMovieRepository
    {
        void SaveMovies(List<Movie> movieList);
        Task<Movie> GetMovie(int movieId);
        void UpdateMoviesStatus();
        Task<List<Movie>> GetActiveMovies(int pageIndex);
        Task<int> GetActiveMoviesTotalPage();
        Task<bool> IsMovieExists(int movieId);
    }
}
