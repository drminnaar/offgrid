using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Offgrid.Framework.EntityFrameworkCore.Extensions;

public static partial class EntityFrameworkCoreExtensions
{
    public static IServiceCollection AddOffgridDbContext<TContext, TImplementation>(
        this IServiceCollection services,
        IConfiguration configuration,
        bool enableDetailedErrors = false,
        bool enableSensitiveDataLogging = false) where TContext : class where TImplementation : DbContext, TContext
    {
        const string ConnectionStringName = "Offgrid";

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{ConnectionStringName}' not found.");
        }

        services.AddDbContext<TContext, TImplementation>(options =>
        {
            options.EnableDetailedErrors(enableDetailedErrors);
            options.EnableSensitiveDataLogging(enableSensitiveDataLogging);
            options.UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorCodesToAdd: null);
            });
        });

        return services;
    }
}
