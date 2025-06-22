using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SertaoArch.QueueServiceRMQ;
using SertaoArch.UserMi.Application.Interfaces;

namespace SertaoArch.Worker.Comsumers
{
    public abstract class Consumer : QueueService, IConsumerService
    {
        public readonly string _queueName;
        private readonly ILogger<Consumer> _logger;

        public Consumer(IConfiguration configuration, ILogger<Consumer> logger, string queueTag) : base(configuration)
        {
            _queueName = configuration[$"RabbitMQ:Queues:{queueTag}"]!;
            _logger = logger;
        }

        public async Task ProcessAsync(string message, CancellationToken cancellation)
        {
            try
            {
                await Execute(message, cancellation);
                _logger.LogInformation("Waiting for messages from RabbitMQ queue '{QueueName}'...", _queueName);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Message consumption canceled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while consuming messages from RabbitMQ.");
            }
        }

        public abstract Task Execute(string message, CancellationToken cancellation);
    }
}
