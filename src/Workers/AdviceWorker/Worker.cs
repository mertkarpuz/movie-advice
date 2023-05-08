using MovieAdvice.Application.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using MovieAdvice.Application.ConfigModels;
using System.Text.Json;
using System.Text;
using MovieAdvice.Application.Dtos.Email;

namespace AdviceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Configuration configuration;
        private readonly IEmailService emailService;
        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;
        public Worker(Configuration configuration, ILogger<Worker> logger, IEmailService emailService)
        {
            _logger = logger;
            this.configuration = configuration;
            this.emailService = emailService;
            connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(configuration.RabbitMqConfiguration.Url)
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            consumer = new(channel);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            bool emailStatus = false;
            consumer.Received += async (model, data) =>
            {
                byte[] body = data.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                EmailDto? sendPasswordRequestLinkDto = JsonSerializer.Deserialize<EmailDto>(message);
                if (sendPasswordRequestLinkDto != null)
                {
                    emailStatus = await emailService.SendEmailAsync(sendPasswordRequestLinkDto);
                    if (emailStatus)
                    {
                        Console.WriteLine("Movie advice mail sent, Info : " + sendPasswordRequestLinkDto.To);
                    }
                }
                Thread.Sleep(60000);
            };
            channel.BasicConsume(queue: configuration.RabbitMqConfiguration.MakeAdviceQueue, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}