namespace Offgrid.ShopApi.DependencyInjection;

public static partial class CommonServiceExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        return services;
    }
}
