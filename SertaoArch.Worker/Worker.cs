using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SertaoArch.Worker.Comsumers;

namespace SertaoArch.Worker
{
    public class Worker<T> : BackgroundService where T : Consumer 
    {
        private readonly T _consumerService;
        private readonly ILogger<Worker<T>> _logger;

        public Worker(T consumerService, ILogger<Worker<T>> logger)
        {
            _logger = logger;
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            await _consumerService.StartListeningAsync(_consumerService._queueName, typeof(T).Name, _consumerService, cancellation)
                 .ContinueWith(task =>
                 {
                     if (task.IsFaulted)
                         _logger.LogError(task.Exception, $"Error while starting the user worker {typeof(T)}.");
                     else
                         _logger.LogInformation($"{typeof(T)} started successfully.");
                 }, cancellation);
        }
    }
}
