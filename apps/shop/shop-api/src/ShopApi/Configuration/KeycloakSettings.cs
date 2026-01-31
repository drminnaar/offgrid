namespace Offgrid.ShopApi.Configuration;

public sealed record KeycloakSettings
{
    public required string Authority { get; init; }
    public required string Audience { get; init; }
    public required bool RequireHttpsMetadata { get; init; }
}
