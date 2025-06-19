using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SertaoArch.QueueServiceRMQ.Interfaces;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; set; }
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddSingleton<IConsumerService, ConsumerService>();
        //services.AddHostedService<ConsumerHostedService>();
    }
}