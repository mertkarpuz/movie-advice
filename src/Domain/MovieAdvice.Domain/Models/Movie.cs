using MovieAdvice.Domain.Common;


namespace MovieAdvice.Domain.Models
{
    public class Movie : BaseModel
    {
        public string? Title { get; set; } = string.Empty;
        public string? ReleaseDate { get; set; } = string.Empty;
        public string? Overview { get; set; } = string.Empty;
        public string? PosterPath { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new();
        public bool Status { get; set; } = true;
    }
}
