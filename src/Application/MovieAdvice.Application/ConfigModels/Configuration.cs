
namespace MovieAdvice.Application.ConfigModels
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new();
        public MovieApiConfigurations MovieApiConfigurations { get; set; } = new();
        public CacheKeys CacheKeys { get; set; } = new();
        public JwtConfiguration JwtConfiguration { get; set; } = new();
        public EmailConfiguration EmailConfiguration { get; set; } = new();
        public RabbitMqConfiguration RabbitMqConfiguration { get; set; } = new();
    }
}
