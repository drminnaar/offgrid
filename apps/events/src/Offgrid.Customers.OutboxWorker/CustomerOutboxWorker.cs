using Offgrid.Customers.OutboxWorker.Application.Services;

namespace Offgrid.Customers.OutboxWorker;

public class CustomerOutboxWorker(
    ILogger<CustomerOutboxWorker> logger,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("CustomerOutboxWorker running at: {time}", DateTimeOffset.Now);
            }
            using var scope = scopeFactory.CreateScope();
            var customerOutboxService = scope.ServiceProvider.GetRequiredService<ICustomerOutboxService>();
            await customerOutboxService.ProcessPendingMessagesAsync(10, stoppingToken);
            await Task.Delay(10000, stoppingToken);
        }
    }
}
