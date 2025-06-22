using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SertaoArch.Contracts.AppObject;

namespace SertaoArch.Worker.Comsumers
{
    public class CreateUserConsumer : Consumer
    {
        public CreateUserConsumer(IConfiguration configuration, ILogger<CreateUserConsumer> logger) : base(configuration, logger, "user_created")
        {
        }

        public override async Task Execute(string message, CancellationToken cancellation)
        {
            await PublishAsync(new AckMessage() { Message = message}, "user_imported", cancellation);
        }
    }
}