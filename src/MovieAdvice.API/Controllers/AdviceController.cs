using DocumentFormat.OpenXml.Spreadsheet;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Dtos.Advice;
using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Application.Dtos.Email;
using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Services;
using MovieAdvice.Application.Validation.FluentValidation.Advice;
using MovieAdvice.Application.Validation.FluentValidation.Comment;
using System.Security.Claims;
using System.Text.Json;

namespace MovieAdvice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        [Route("MakeAdvice")]
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
                logger.LogError("AddComment", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            return Ok("Advice mail send!");
        }
    }
}
