using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Dtos.Advice;
using MovieAdvice.Application.Dtos.Email;
using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Validation.FluentValidation.Advice;
using System.Security.Claims;
using System.Text.Json;

namespace MovieAdvice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdviceController : ControllerBase
    {
        private readonly IMoviesService moviesService;
        private readonly AdviceDtoValidator adviceDtoValidator;
        private ValidationResult validationResult;
        private readonly IRabbitMqHelper rabbitMqHelper;
        private readonly Configuration configuration;
        private readonly ILogger<AdviceController> logger;
        public AdviceController(IMoviesService moviesService, IRabbitMqHelper rabbitMqHelper, Configuration configuration, ILogger<AdviceController> logger)
        {
            this.moviesService = moviesService;
            this.adviceDtoValidator = new(moviesService);
            validationResult = new();
            this.rabbitMqHelper = rabbitMqHelper;
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// With this endpoint, you can recommend the selected movie to your friends by e-mail. (Authorization required)
        /// </summary>
        [HttpPost]
        [Route("MakeAdvice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult> MakeAdvice(AdviceDto adviceDto)
        {
            try
            {
                validationResult = await adviceDtoValidator.ValidateAsync(adviceDto);
                if (validationResult.IsValid)
                {
                    GetMovieDto getMovieDto = await moviesService.GetMovie(adviceDto.MovieId);

                    rabbitMqHelper.Publish(configuration.RabbitMqConfiguration.MakeAdviceQueue,
                            JsonSerializer.Serialize(new EmailDto
                            {
                                MovieTitle = getMovieDto.Title,
                                MovieDescription = getMovieDto.Overview,
                                MovieImage = getMovieDto.PosterPath,
                                Sender = User.Claims.First(x => x.Type == ClaimTypes.Name).Value + " " +
                                    User.Claims.First(x => x.Type == ClaimTypes.Surname).Value,
                                To = adviceDto.ToMailAddress
                            }));
                }
                else
                {
                    return BadRequest(new
                    {
                        validationResult.Errors
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("MakeAdvice", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            return Ok("Advice mail send!");
        }
    }
}
