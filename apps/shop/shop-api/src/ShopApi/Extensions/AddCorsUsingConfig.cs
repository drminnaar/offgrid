using Offgrid.ShopApi.Configuration;

namespace Offgrid.ShopApi.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddCorsUsingConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            var config = configuration
                .GetRequiredSection(nameof(CorsPolicySettings))
                .Get<CorsPolicySettings>()
                ?? throw new InvalidOperationException("CORS configuration is missing.");

            foreach (var policy in config!.Policies)
            {
                options.AddPolicy(policy.Name, builder =>
                    builder
                        .WithOrigins(policy.AllowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            }

            var defaultPolicy = config.Policies.FirstOrDefault(setting => setting.Name == config.DefaultPolicyName)
                ?? throw new InvalidOperationException($"Default CORS policy '{config.DefaultPolicyName}' is not defined in the policies list.");

            options.DefaultPolicyName = defaultPolicy.Name;
        });

        return services;
    }
}
