using Microsoft.IdentityModel.Tokens;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieAdvice.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly Configuration configuration;
        public JwtService(Configuration configuration)
        {
            this.configuration = configuration;
        }
        public JwtAccessToken GenerateJwt(User user)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration.JwtConfiguration.SecurityKey));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new(issuer: configuration.JwtConfiguration.Issuer, audience: configuration.JwtConfiguration.Audience,
                notBefore: DateTime.Now, expires: DateTime.Now.AddHours(configuration.JwtConfiguration.Expiration),
                signingCredentials: signingCredentials, claims: GetClaims(user));

            JwtSecurityTokenHandler handler = new();

            return new JwtAccessToken { Token = handler.WriteToken(jwtSecurityToken) };
        }

        public List<Claim> GetClaims(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.Surname)
            };
            return claims;
        }
    }
}
