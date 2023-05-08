using MovieAdvice.Domain.Common;


namespace MovieAdvice.Domain.Models
{
    public class Comment : BaseModel
    {
        public int Point { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
