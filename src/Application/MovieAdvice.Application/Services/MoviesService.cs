using AutoMapper;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.ApiModels.MovieApi;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Domain.Models;


namespace MovieAdvice.Application.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;
        public MoviesService(IMovieRepository movieRepository,IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;   
        }

        public async Task<List<Movie>> GetActiveMovies()
        {
            return await movieRepository.GetActiveMovies();
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            return await movieRepository.GetMovie(movieId);
        }

        public void SaveMovie(List<MovieApiModel> movieApiModelList)
        {
            movieRepository.SaveMovies(mapper.Map<List<Movie>>(movieApiModelList));
        }

        public void UpdateMoviesStatus()
        {
            movieRepository.UpdateMoviesStatus();
        }
    }
}
