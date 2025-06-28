using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SertaoArch.Contracts;
using SertaoArch.QueueServiceRMQ;
using SertaoArch.UserMi.Application.Interfaces;

namespace SertaoArch.Worker.Comsumers
{
    public abstract class Consumer<T> : QueueService, IConsumerService 
        where T : ContractBase<long>
    {
        protected readonly IMongoDatabase _database;
        public readonly string _queueName;
        private readonly ILogger<Consumer<T>> _logger;

        public Consumer(IConfiguration configuration, ILogger<Consumer<T>> logger, IMongoClient client, string queueTag) : base(configuration)
        {
            _database = client.GetDatabase("UserMiDb");
            _queueName = configuration[$"RabbitMQ:Queues:{queueTag}"]!;
            _logger = logger;
        }

        public async Task ProcessAsync(string message, CancellationToken cancellation)
        {
            try
            {
                var contract = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);

                await Execute(contract, cancellation);
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

        public abstract Task Execute(T message, CancellationToken cancellation);
    }
}
