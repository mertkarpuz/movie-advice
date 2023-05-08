using MovieAdvice.Application.Dtos.User;


namespace MovieAdvice.Application.Dtos.Comment
{
    public class CommentDto
    {
        public double Point { get; set; }
        public string Description { get; set; } = string.Empty;
        public UserDto User { get; set; }
        public DateTime Date { get; set; }
    }
}
