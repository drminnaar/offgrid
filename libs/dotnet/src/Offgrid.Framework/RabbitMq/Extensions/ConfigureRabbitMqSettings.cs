using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Offgrid.Framework.RabbitMq.Extensions;

public static partial class RabbitMqExtensions
{
    public static IServiceCollection ConfigureRabbitMqSettings(
        this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection(RabbitMqClientOptions.SectionName);
        services.Configure<RabbitMqClientOptions>(section);
        return services;
    }
}
