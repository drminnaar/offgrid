using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using Offgrid.Portal.Customers.OutboxProcessor.Application.Services;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

public sealed class LoggingPublisher : IEventPublisher
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

        // 3. Encode to JSON (as a ReadOnlyMemory<byte> or Stream)
        var bytes = formatter.EncodeStructuredModeMessage(cloudEvent, out var contentType);
        var jsonString = System.Text.Encoding.UTF8.GetString(bytes.ToArray());

        _logger.LogInformation("Event data: {EventData}", jsonString);

        return Task.CompletedTask;
    }
}
