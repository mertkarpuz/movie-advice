
namespace MovieAdvice.Application.ConfigModels
{
    public class RabbitMqConfiguration
    {
        public string Url { get; set; } = string.Empty;
        public string MakeAdviceQueue { get; set; } = string.Empty;
    }
}
