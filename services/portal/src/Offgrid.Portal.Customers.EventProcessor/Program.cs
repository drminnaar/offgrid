using Microsoft.Extensions.Logging.Console;
using Offgrid.Portal.Customers.EventProcessor;
using Offgrid.Portal.Customers.EventProcessor.Extensions;

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

builder.Services.AddRabbitMqServices(builder.Configuration);

builder.Services.AddConsumerServices();

builder.Services.AddHostedService<CustomerChangedEventWorker>();
builder.Services.AddHostedService<CustomerSuspendedEventWorker>();
builder.Services.AddHostedService<CustomerReinstatedEventWorker>();

var host = builder.Build();
host.Run();
