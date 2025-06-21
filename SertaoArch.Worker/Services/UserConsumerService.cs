using SertaoArch.QueueServiceRMQ;
using Microsoft.Extensions.Configuration;
using SertaoArch.UserMi.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace SertaoArch.Worker.Services
{
    public class UserConsumerService : QueueService, IConsumerService
    {
        public readonly string _queueName;
        private readonly ILogger<UserConsumerService> _logger;

        public UserConsumerService(IConfiguration configuration, ILogger<UserConsumerService> logger) : base(configuration)
        {
            _queueName = configuration["RabbitMQ:Queues:user_created"] ?? "user-created";
            _logger = logger;
        }
        
        public async Task ProcessAsync(string message, CancellationToken cancellation)
        {
            try
            {
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
    }
}