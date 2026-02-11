using Microsoft.Extensions.Logging.Console;
using Offgrid.Portal.Customers.OutboxProcessor;
using Offgrid.Portal.Customers.OutboxProcessor.Extensions;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence;
using Offgrid.Framework.EntityFrameworkCore.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = true;
    options.TimestampFormat = "[HH:mm:ss] ";
    options.ColorBehavior = LoggerColorBehavior.Enabled;
    options.IncludeScopes = true;
});
builder.Logging.AddDebug();

builder.Services.AddOffgridDbContext<IOutboxDbContext, OutboxDbContext>(
    builder.Configuration,
    enableDetailedErrors: !builder.Environment.IsProduction(),
    enableSensitiveDataLogging: !builder.Environment.IsProduction());

builder.Services.AddCustomerServices();

builder.Services.AddHostedService<CustomerOutboxWorker>();

var host = builder.Build();

host.Run();
