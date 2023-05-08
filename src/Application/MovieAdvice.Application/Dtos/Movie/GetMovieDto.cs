using MovieAdvice.Application.Dtos.Comment;

namespace MovieAdvice.Application.Dtos.Movie
{
    public class GetMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new();
        public double MovieScore { get; set; }
    }
}
