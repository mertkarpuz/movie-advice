

namespace MovieAdvice.Application.Dtos.Email
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
        public string MovieDescription { get; set; } = string.Empty;
        public string MovieImage { get; set; } = string.Empty;
        public string Sender { get; set; }
    }
}
