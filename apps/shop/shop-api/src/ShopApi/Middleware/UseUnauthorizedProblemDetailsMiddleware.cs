namespace Offgrid.ShopApi.Middleware;

public static partial class MiddlewareExtensions
{
    public static WebApplication UseUnauthorizedProblemDetailsMiddleware(this WebApplication app)
    {
        app.UseMiddleware<UnauthorizedProblemDetailsMiddleware>();
        return app;
    }
}
