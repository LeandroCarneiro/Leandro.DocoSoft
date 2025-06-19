using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using SertaoArch.UserMi.Application.Interface;
using SertaoArch.Contracts;

namespace SertaoArch.QueueServiceRMQ
{
    public class QueueService : IQueueService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConfiguration _configuration;

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;

            _factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"]!,
                Port = int.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:UserName"]!,
                Password = _configuration["RabbitMQ:Password"]!
            };
        }

        public async Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : ContractBase<long>
        {
            using var connection = await _factory.CreateConnectionAsync(cancellationToken);
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: queueName,
                                 body: body,
                                 cancellationToken);

            Console.WriteLine($"[x] Sent {JsonSerializer.Serialize(message)} to queue {queueName}");

            await Task.CompletedTask;
        }
    }
}