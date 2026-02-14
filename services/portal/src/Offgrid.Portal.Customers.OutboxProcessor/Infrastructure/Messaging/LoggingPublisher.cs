using System.Text;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using Offgrid.Framework.Messaging;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

public sealed class LoggingPublisher : IEventPublisher<CloudEvent>
{
    private readonly ILogger<LoggingPublisher> _logger;

    public LoggingPublisher(ILogger<LoggingPublisher> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    public Task PublishAsync(CloudEvent cloudEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Processing cloud event {CloudEventId} for type {CloudEventTypeId}",
            cloudEvent.Id,
            cloudEvent.Type);

        var formatter = new JsonEventFormatter();
        var bytes = formatter.EncodeStructuredModeMessage(cloudEvent, out var _);
        var jsonString = Encoding.UTF8.GetString(bytes.ToArray());

        _logger.LogInformation("Event data: {EventData}", jsonString);

        return Task.CompletedTask;
    }
}
