using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using SertaoArch.UserMi.Application.Interface;
using SertaoArch.Contracts;
using RabbitMQ.Client.Events;
using SertaoArch.UserMi.Application.Interfaces;
using System.Threading;

namespace SertaoArch.QueueServiceRMQ
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _factory;

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;
            _factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"] ?? "localhost",
                Port = int.TryParse(_configuration["RabbitMQ:Port"], out var port) ? port : 5672,
                UserName = _configuration["RabbitMQ:UserName"] ?? "admin",
                Password = _configuration["RabbitMQ:Password"] ?? "admin"
            };
        }

        private async Task<IChannel> CreateChannelAsync(string queueName, CancellationToken cancellation)
        {
            var connection = await _factory.CreateConnectionAsync(cancellation);
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            return channel;
        }

        public async Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : ContractBase<long>
        {
            using (var channel = await CreateChannelAsync(queueName, cancellationToken))
            {
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                await channel.BasicPublishAsync("", routingKey: queueName, body: body, cancellationToken);

                Console.WriteLine($"[x] Sent {JsonSerializer.Serialize(message)} to queue {queueName}");

                await Task.CompletedTask;
            }
        }

        public async Task StartListeningAsync(string queueName, string consumerName, IConsumerService consumerService, CancellationToken cancellation)
        {
            var channel = await CreateChannelAsync(queueName, cancellation);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea)  => 
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await consumerService.ProcessAsync(message, cancellation);
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queue: queueName, consumerTag: consumerName, autoAck: false, consumer: consumer);
            Console.WriteLine($"Listening to queue: {queueName}");
        }
    }
}