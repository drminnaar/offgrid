using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Offgrid.ShopApi.Configuration;

namespace Offgrid.ShopApi.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var settings = configuration
                .GetRequiredSection(nameof(KeycloakSettings))
                .Get<KeycloakSettings>()
                ?? throw new InvalidOperationException("Keycloak configuration settings is missing or not configured correctly.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var authority = settings.Authority;
                var audience = settings.Audience;
                var requireHttpsMetadata = settings.RequireHttpsMetadata;

                options.Authority = authority;
                options.Audience = audience;
                options.RequireHttpsMetadata = requireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authority,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero,

                    // Map Keycloak's 'sub' to NameIdentifier
                    NameClaimType = "preferred_username",
                    RoleClaimType = ClaimTypes.Role
                };


                if (!environment.IsProduction())
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = HandleAuthenticationFailed,
                        OnTokenValidated = HandleTokenValidated,
                        OnChallenge = HandleChallenge,
                        OnForbidden = HandleForbidden,
                        OnMessageReceived = HandleMessageReceived
                    };
                }
            });
        return services;
    }

    private static Task HandleAuthenticationFailed(AuthenticationFailedContext context)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        logger.LogError(
            context.Exception,
            "Authentication failed for {Scheme}. Path: {Path}, Method: {Method}, Error: {Error}",
            context.Scheme.Name,
            context.HttpContext.Request.Path,
            context.HttpContext.Request.Method,
            context.Exception.Message);

        // Log additional failure details if available
        if (context.Exception is SecurityTokenException securityTokenException)
        {
            logger.LogError(
                "Security token validation failed: {Details}",
                securityTokenException.Message);
        }

        return Task.CompletedTask;
    }

    private static Task HandleTokenValidated(TokenValidatedContext context)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        var principal = context.Principal;
        var claims = principal?.Claims.ToList() ?? new List<Claim>();

        // Extract key claim values
        var username = principal?.Identity?.Name ?? "Unknown";
        var subject = claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value ?? "N/A";
        var roles = string.Join(", ", claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
        var expiration = claims.FirstOrDefault(c => c.Type == "exp")?.Value ?? "N/A";
        var issuer = context.SecurityToken.Issuer;

        logger.LogInformation(
            "Token validated successfully. User: {User}, Subject: {Subject}, Roles: {Roles}, " +
            "Issuer: {Issuer}, Expiration: {Expiration}, Claims Count: {ClaimCount}",
            username,
            subject,
            string.IsNullOrEmpty(roles) ? "None" : roles,
            issuer,
            expiration,
            claims.Count);

        return Task.CompletedTask;
    }

    private static Task HandleChallenge(JwtBearerChallengeContext context)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        logger.LogWarning(
            "Authentication challenge issued. Path: {Path}, Method: {Method}, " +
            "Error: {Error}, ErrorDescription: {ErrorDescription}",
            context.HttpContext.Request.Path,
            context.HttpContext.Request.Method,
            context.Error ?? "None",
            context.ErrorDescription ?? "None");

        return Task.CompletedTask;
    }

    private static Task HandleForbidden(ForbiddenContext context)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        var username = context.Principal?.Identity?.Name ?? "Unknown";
        var isAuthenticated = context.Principal?.Identity?.IsAuthenticated ?? false;

        logger.LogWarning(
            "Access forbidden for user: {User}, Authenticated: {IsAuthenticated}, " +
            "Path: {Path}, Method: {Method}",
            username,
            isAuthenticated,
            context.HttpContext.Request.Path,
            context.HttpContext.Request.Method);

        return Task.CompletedTask;
    }

    private static Task HandleMessageReceived(MessageReceivedContext context)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        var authHeader = context.Request.Headers.Authorization.ToString();
        var hasToken = !string.IsNullOrEmpty(context.Token);
        var tokenPreview = hasToken && context.Token!.Length > 20
            ? $"{context.Token.Substring(0, 20)}..."
            : "None";

        logger.LogDebug(
            "Message received. Path: {Path}, Has Authorization Header: {HasAuthHeader}, " +
            "Has Token: {HasToken}, Token Preview: {TokenPreview}",
            context.HttpContext.Request.Path,
            !string.IsNullOrEmpty(authHeader),
            hasToken,
            tokenPreview);

        return Task.CompletedTask;
    }
}
