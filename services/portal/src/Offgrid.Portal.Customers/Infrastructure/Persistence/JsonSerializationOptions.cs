using System.Text.Json;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence;

internal static class JsonSerializationOptions
{
    internal static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
