

namespace MovieAdvice.Application.Interfaces
{
    public interface IRabbitMqHelper
    {
        void Publish(string queueName, string data);
    }
}
