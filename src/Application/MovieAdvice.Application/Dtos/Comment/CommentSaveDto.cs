
namespace MovieAdvice.Application.Dtos.Comment
{
    public class CommentSaveDto
    {
        public int Point { get; set; }
        public string Description { get; set; } = "";
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
