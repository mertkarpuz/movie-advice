﻿
namespace MovieAdvice.Application.ConfigModels
{
    public class JwtConfiguration
    {
        public string SecurityKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int Expiration { get; set; }
    }
}
