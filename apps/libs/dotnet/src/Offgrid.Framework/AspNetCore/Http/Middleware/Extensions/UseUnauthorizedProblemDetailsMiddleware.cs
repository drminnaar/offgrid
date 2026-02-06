using Microsoft.AspNetCore.Builder;

namespace Offgrid.Framework.AspNetCore.Http.Middleware.Extensions;

public static partial class MiddlewareExtensions
{
    public static WebApplication UseUnauthorizedProblemDetailsMiddleware(this WebApplication app)
    {
        app.UseMiddleware<UnauthorizedProblemDetailsMiddleware>();
        return app;
    }
}
