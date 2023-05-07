using MovieAdvice.Domain.Models;
using System.Security.Claims;

namespace MovieAdvice.Application.Interfaces
{
    public interface IJwtService
    {
        JwtAccessToken GenerateJwt(User user);
        List<Claim> GetClaims(User user);
    }
}
