using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SertaoArch.Worker.Services;


public class Startup
{
    public IConfiguration Configuration { get; set; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedService<UserWorker>();
        services.AddTransient<UserConsumerService>();

        services.AddSingleton(async sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = Configuration["RabbitMQ:HostName"] ?? "localhost",
                Port = int.TryParse(Configuration["RabbitMQ:Port"], out var port) ? port : 5672,
                UserName = Configuration["RabbitMQ:UserName"] ?? "guest",
                Password = Configuration["RabbitMQ:Password"] ?? "guest"
            };

            return await factory.CreateConnectionAsync();
        });

        services.AddScoped(async sp =>
        {
            var connection = sp.GetRequiredService<IConnection>();
            return await connection.CreateChannelAsync();
        });

        services.AddHealthChecks().AddRabbitMQ(provider =>
        provider.GetRequiredService<IConnection>());
    }
}
