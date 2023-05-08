using Microsoft.AspNetCore.Mvc;
using MovieAdvice.Application.Dtos.Login;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.Models;

namespace MovieAdvice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtService jwtService;
        private readonly ILogger<AuthController> logger;
        public AuthController(IUserService userService, IJwtService jwtService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            this.jwtService = jwtService;
            this.logger = logger;
        }


        /// <summary>
        /// With this endpoint, user will log-in to the application
        /// </summary>
        [HttpPost]
        [Route("Login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(JwtAccessToken), 201)]

        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var user = await userService.GetUserByEmail(loginDto.Email);
                if (user != null && userService.CheckPassword(loginDto.Password, user.Password))
                {
                    return Created("", jwtService.GenerateJwt(user));
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Login", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            return BadRequest("Email or password wrong!");
        }
    }
}
