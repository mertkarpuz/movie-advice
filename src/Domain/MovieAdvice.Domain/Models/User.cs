﻿using MovieAdvice.Domain.Common;


namespace MovieAdvice.Domain.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
