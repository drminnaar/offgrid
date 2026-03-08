using Microsoft.AspNetCore.Http;

namespace Offgrid.Framework.AspNetCore.Http.Extensions;

public static partial class HttpExtensions
{
    public static string Username(this HttpContext httpContext)
    {
        return httpContext.User?.Identity?.Name ?? string.Empty;
    }

    public static string RequiredUsername(this HttpContext httpContext)
    {
        var username = httpContext.User?.Identity?.Name;
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        return username;
    }
}
