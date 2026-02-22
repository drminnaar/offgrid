using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoInit.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<MongoInit.Configuration.DatabaseOptions>(
    builder.Configuration.GetSection(MongoInit.Configuration.DatabaseOptions.ConfigurationSectionName));

builder.Services.Configure<MongoInit.Configuration.FileOptions>(
    builder.Configuration.GetSection(MongoInit.Configuration.FileOptions.ConfigurationSectionName));

builder.Services.AddSingleton<ProductCollection>();

builder.Services.AddSingleton<ProductFile>();

builder.Services.AddSingleton<Seeder>();

var app = builder.Build();

// Seeder logic (run once at startup)
try
{
    var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

    var writeFile = args?.Length > 0 && args[0]?.ToLowerInvariant() == "--writefile";
    await seeder.SeedAsync(environment, writeFile);
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "❌ An error occurred during the seeding process.");
}

// Do not call `app.Run()` - exit after seeding has completed.
// The host is not started as a long-running service in this workflow.
