using CloudNative.CloudEvents;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.RabbitMq;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

public sealed class MessageBusPublisher : IEventPublisher<CloudEvent>
{
    private readonly ILogger<MessageBusPublisher> _logger;

    private readonly RabbitMqCloudEventPublisher _publisher;

    public MessageBusPublisher(
        ILogger<MessageBusPublisher> logger,
        RabbitMqCloudEventPublisher publisher)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(publisher, nameof(publisher));
        _logger = logger;
        _publisher = publisher;
    }

    public async Task PublishAsync(CloudEvent cloudEvent, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cloudEvent, nameof(cloudEvent));
        try
        {
            await _publisher.PublishAsync(cloudEvent, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish event with id {EventId} and type {EventType}", cloudEvent.Id, cloudEvent.Type);
            throw;
        }
    }
}
