using Microsoft.Extensions.Hosting;
using SertaoArch.QueueServiceRMQ.Interfaces;

public class ConsumerHostedService : BackgroundService
{
    private readonly IConsumerService _consumerService;

    public ConsumerHostedService(IConsumerService consumerService)
    {
        _consumerService = consumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        await _consumerService.ReadMessgaesAsync(cancellation);
    }
}