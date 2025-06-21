using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SertaoArch.Worker.Services;

public class UserWorker : BackgroundService
{
    private readonly UserConsumerService _consumerService;
    private readonly ILogger<UserWorker> _logger;

    public UserWorker(UserConsumerService consumerService, ILogger<UserWorker> logger)
    {
        _logger = logger;
        _consumerService = consumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
       await _consumerService.StartListeningAsync(_consumerService._queueName, "UserWorker", _consumerService, cancellation)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    _logger.LogError(task.Exception, "Error while starting the user worker.");
                }
                else
                {
                    _logger.LogInformation("User worker started successfully.");
                }
            }, cancellation);
    }
}
