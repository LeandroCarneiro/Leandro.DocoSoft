using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SertaoArch.Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var host = Host.CreateDefaultBuilder(args)
            //    .ConfigureServices((hostContext, services) =>
            //    {
            //        services.AddHostedService<ConsumerService>();
            //    })
            //    .Build();

            //await host.RunAsync();
        }
    }

    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumer started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                // Simulate consuming a message
                _logger.LogInformation("Consuming message at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("Consumer stopped.");
        }
    }
}
