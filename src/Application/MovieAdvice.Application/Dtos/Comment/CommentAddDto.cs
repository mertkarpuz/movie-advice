
namespace MovieAdvice.Application.Dtos.Comment
{
    public class CommentAddDto
    {
        public int Point { get; set; }
        public string Description { get; set; } = "";
        public int MovieId { get; set; }
    }
}
