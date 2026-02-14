using Microsoft.Extensions.Options;
using Offgrid.Framework.RabbitMq;
using Offgrid.Framework.RabbitMq.Extensions;
using RabbitMQ.Client;

namespace Offgrid.Portal.Customers.EventProcessor.Extensions;

public static partial class Extensions
{
    public static void AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureRabbitMqSettings(configuration);

        services.AddSingleton<IConnectionFactory>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<RabbitMqClientOptions>>().Value;
            return new ConnectionFactory
            {
                HostName = settings.HostName,
                Port = settings.Port,
                UserName = settings.UserName,
                Password = settings.Password,
                VirtualHost = settings.VirtualHost,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                RequestedHeartbeat = TimeSpan.FromSeconds(30)
            };
        });
    }
}
