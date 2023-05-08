using Microsoft.AspNetCore.Mvc;
using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Application.Interfaces;

namespace MovieAdvice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> logger;
        private readonly IMoviesService moviesService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesService moviesService)
        {
            this.logger = logger;
            this.moviesService = moviesService;
        }

        /// <summary>
        /// With this endpoint, you can access the movies that are being shown in the cinema. (This endpoint paginated)
        /// </summary>
        /// /// <param name="page"></param>
        [HttpGet]
        [Route("GetMovies")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(MovieListInfoDto), 200)]

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


        /// <summary>
        /// With this endpoint, you can access the selected movie with movie id. (includes comments, points, users)
        /// </summary>
        /// /// <param name="movieId"></param>
        [HttpGet]
        [Route("GetMovie")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(GetMovieDto), 200)]
        public async Task<ActionResult> GetMovie(int movieId)
        {
            GetMovieDto getMovieDto = new();
            try
            {
                getMovieDto = await moviesService.GetMovie(movieId);
                if (getMovieDto == null)
                {
                    return BadRequest("Movie not found!");
                }
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