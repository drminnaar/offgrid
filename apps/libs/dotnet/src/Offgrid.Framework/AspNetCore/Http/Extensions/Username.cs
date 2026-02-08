using Microsoft.AspNetCore.Http;

namespace Offgrid.Framework.AspNetCore.Http.Extensions;

public static partial class HttpExtensions
{
    public static string Username(this HttpContext httpContext)
    {
        return httpContext.User?.Identity?.Name ?? string.Empty;
    }
}
