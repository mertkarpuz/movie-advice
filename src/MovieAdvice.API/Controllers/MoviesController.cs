using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Application.Dtos.Email;
using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Application.Interfaces;
using System.IO;


namespace MovieAdvice.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> logger;
        private readonly IMoviesService moviesService;


        public MoviesController(ILogger<MoviesController> logger, IMoviesService moviesService)
        {
            this.logger = logger;
            this.moviesService = moviesService;
        }

        [HttpGet]
        [Route("GetMovies")]
        
        public async Task<ActionResult> GetMovies(int page = 1)
        {
            List<MovieListDto> movieList = new();
            int totalPage = 0;
            try
            {
                movieList = await moviesService.GetActiveMovies(page);
                totalPage = await moviesService.GetActiveMoviesTotalPage();

            }
            catch (Exception ex)
            {
                logger.LogError("GetMovies", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            return Ok(new MovieListInfoDto { TotalPage = totalPage, Page = page, Movies = movieList });
        }

        [HttpGet]
        [Route("GetMovie")]
        public async Task<ActionResult> GetMovie(int movieId)
        {
            GetMovieDto getMovieDto = new();
            try
            {
                getMovieDto = await moviesService.GetMovie(movieId);
            }
            catch (Exception ex)
            {
                logger.LogError("GetMovie", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            return Ok(getMovieDto);
        }

        
    }
}