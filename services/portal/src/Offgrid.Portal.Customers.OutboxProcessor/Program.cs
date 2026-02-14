using Microsoft.Extensions.Logging.Console;
using Offgrid.Portal.Customers.OutboxProcessor;
using Offgrid.Portal.Customers.OutboxProcessor.Extensions;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence;
using Offgrid.Framework.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Offgrid.Framework.RabbitMq.Extensions;
using Offgrid.Framework.RabbitMq;

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

builder.Services.ConfigureRabbitMqSettings(builder.Configuration);

builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<RabbitMqClientOptions>>().Value;
    return new ConnectionFactory
    {
        HostName = settings.HostName,
        Port = settings.Port,
        UserName = settings.UserName,
        Password = settings.Password,
        VirtualHost = settings.VirtualHost,
        AutomaticRecoveryEnabled = true,
        NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
        RequestedHeartbeat = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddCustomerServices();

builder.Services.AddHostedService<CustomerOutboxWorker>();

var host = builder.Build();

host.Run();
