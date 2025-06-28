using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SertaoArch.Contracts;
using SertaoArch.Worker.Comsumers;

namespace SertaoArch.Worker
{
    public class Worker<T,TC> : BackgroundService
    where T : Consumer<TC>
    where TC : ContractBase<long>
    {
        private readonly T _consumerService;
        private readonly ILogger<Worker<T,TC>> _logger;

        public Worker(T consumerService, ILogger<Worker<T,TC>> logger)
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
