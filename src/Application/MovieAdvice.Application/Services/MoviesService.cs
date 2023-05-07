using AutoMapper;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Dtos.Movie;
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
        private readonly ICacheService cacheService;
        private readonly Configuration configuration;
        public MoviesService(IMovieRepository movieRepository, IMapper mapper, ICacheService cacheService, Configuration configuration)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
            this.cacheService = cacheService;
            this.configuration = configuration;
        }

        public async Task<List<MovieListDto>> GetActiveMovies(int pageIndex)
        {
            List<MovieListDto> cachedList = await cacheService.GetData<List<MovieListDto>>(configuration.CacheKeys.ActiveMoviesKey + pageIndex.ToString());

            if (cachedList != null)
            {
                return cachedList;
            }

            List<MovieListDto> movieList = mapper.Map<List<MovieListDto>>(await movieRepository.GetActiveMovies(pageIndex));

            await cacheService.SetData(configuration.CacheKeys.ActiveMoviesKey + pageIndex.ToString(), movieList,
                DateTimeOffset.Now.AddMinutes(2));

            return movieList;
        }

        public async Task<int> GetActiveMoviesTotalPage()
        {
            return await movieRepository.GetActiveMoviesTotalPage();
        }

        public async Task<GetMovieDto> GetMovie(int movieId)
        {
            GetMovieDto getMovieDto = mapper.Map<GetMovieDto>(await movieRepository.GetMovie(movieId));
            getMovieDto.MovieScore = Math.Round(getMovieDto.Comments.Sum(x => x.Point) / getMovieDto.Comments.Count, 1);
            return getMovieDto;
        }

        public Task<bool> IsMovieExists(int movieId)
        {
            return movieRepository.IsMovieExists(movieId);
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
