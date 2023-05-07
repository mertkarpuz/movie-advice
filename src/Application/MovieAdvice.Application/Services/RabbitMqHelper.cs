using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Utilities;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Services
{
    public class RabbitMqHelper : IRabbitMqHelper
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IModel channel;
        private readonly Configuration configuration;
        public RabbitMqHelper(Configuration configuration)
        {
            this.configuration = configuration;
            connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(configuration.RabbitMqConfiguration.Url)
            };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Publish(string queueName, string data)
        {
            channel.QueueDeclare(queueName, false, false, false);
            var body = Converters.StringToByteConverter(data);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}
