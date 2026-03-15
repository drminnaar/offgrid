using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Offgrid.Shop.Products.Application.Services;
using Offgrid.Shop.Products.Infrastructure.Config;
using Offgrid.Shop.Products.Infrastructure.Search;
using Typesense.Setup;

namespace Offgrid.Shop.Products.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddTypeSenseInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services
            .AddOptions<TypesenseOptions>()
            .Bind(configuration.GetRequiredSection(TypesenseOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey), "Typesense ApiKey is required.")
            .Validate(options => options.Nodes.Count > 0, "At least one Typesense node must be configured.")
            .ValidateOnStart();

        services.AddTypesenseClient(config =>
        {
            var options = configuration
                .GetRequiredSection(TypesenseOptions.SectionName)
                .Get<TypesenseOptions>()
                ?? throw new InvalidOperationException("Failed to bind Typesense options.");

            config.ApiKey = options.ApiKey;
            config.Nodes = options.Nodes;
        });

        services.AddSingleton<IProductSearchService, TypesenseProductSearchService>();

        return services;
    }
}
