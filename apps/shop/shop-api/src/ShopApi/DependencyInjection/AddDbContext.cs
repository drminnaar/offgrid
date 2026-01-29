using Microsoft.EntityFrameworkCore;
using Offgrid.Customers.Infrastructure.Persistence;

namespace Offgrid.ShopApi.DependencyInjection;

public static partial class CommonServiceExtensions
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        const string ConnectionStringName = "Offgrid";

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{ConnectionStringName}' not found.");
        }

        services.AddDbContextPool<IAppDbContext, AppDbContext>(options =>
        {
            options.EnableDetailedErrors(environment.IsDevelopment());
            options.EnableSensitiveDataLogging(environment.IsDevelopment());
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
