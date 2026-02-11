namespace Offgrid.Framework.Configuration;

public sealed record CorsPolicySettings
{
    public required string DefaultPolicyName { get; init; }
    public required CorsPolicy[] Policies { get; init; }
}

public sealed record CorsPolicy
{
    public required string Name { get; init; }
    public required string[] AllowedOrigins { get; init; }
}
