using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Validation.FluentValidation.Comment;
using System.Security.Claims;

namespace MovieAdvice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly ILogger<CommentController> logger;
        private readonly CommentAddDtoValidator commentAddDtoValidator;
        private readonly IMoviesService moviesService;
        private readonly IMapper mapper;
        private ValidationResult validationResult;
        public CommentController(ICommentService commentService, ILogger<CommentController> logger, IMoviesService moviesService, IMapper mapper)
        {
            this.commentService = commentService;
            this.logger = logger;
            this.moviesService = moviesService;
            commentAddDtoValidator = new(moviesService);
            validationResult = new();
            this.mapper = mapper;
        }


        /// <summary>
        /// With this endpoint, you can add points and comments to the selected movie. (Authorization is required)
        /// </summary>
        [HttpPost]
        [Route("AddComment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(CommentAddDto), 201)]
        public async Task<ActionResult> AddComment(CommentAddDto commentAddDto)
        {
            try
            {
                validationResult = await commentAddDtoValidator.ValidateAsync(commentAddDto);
                if (validationResult.IsValid)
                {
                    CommentSaveDto commentSaveDto = mapper.Map<CommentSaveDto>(commentAddDto);
                    commentSaveDto.UserId = Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                    await commentService.SaveComment(commentSaveDto);
                    return Created("", commentAddDto);
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError("AddComment", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }

            return BadRequest(new
            {
                validationResult.Errors
            });

        }
    }
}
