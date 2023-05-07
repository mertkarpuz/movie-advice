using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAdvice.Application.Dtos.Login;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Services;

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

        [HttpPost]
        [Route("Login")]
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
