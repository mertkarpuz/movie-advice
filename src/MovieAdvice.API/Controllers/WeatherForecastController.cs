using Microsoft.AspNetCore.Mvc;
using MovieAdvice.Application.Interfaces;

namespace MovieAdvice.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGetMoviesService getMoviesService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGetMoviesService getMoviesService)
        {
            _logger = logger;
            this.getMoviesService = getMoviesService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult> Get()
        {
            return Ok( );
        }
    }
}