using System.Text.Json;
using CloudNative.CloudEvents;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

public interface ICloudEventFactory
{
    CloudEvent CreateCloudEvent(string type, string eventJson);
    CloudEvent CreateCloudEvent(IDomainEvent domainEvent);
}

public sealed class CloudEventFactory : ICloudEventFactory
{
    private readonly ICloudEventIdProvider _idProvider;
    private static readonly IReadOnlyDictionary<string, Type> _typeMap =
        new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            [nameof(CustomerChangedEvent)] = typeof(CustomerChangedEvent),
            [nameof(CustomerReinstatedEvent)] = typeof(CustomerReinstatedEvent),
            [nameof(CustomerSuspendedEvent)] = typeof(CustomerSuspendedEvent),
        };

    public CloudEventFactory(ICloudEventIdProvider idProvider)
    {
        ArgumentNullException.ThrowIfNull(idProvider, nameof(idProvider));
        _idProvider = idProvider;
    }

    public CloudEvent CreateCloudEvent(string type, string eventJson)
    {
        if (!_typeMap.TryGetValue(type, out var eventType))
        {
            throw new InvalidOperationException($"Unsupported domain event type: {type}");
        }

        if (JsonSerializer.Deserialize(eventJson, eventType) is not IDomainEvent domainEvent)
        {
            throw new InvalidOperationException($"Failed to deserialize domain event of type: {type}");
        }

        return CreateCloudEvent(domainEvent);
    }

    public CloudEvent CreateCloudEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));

        var cloudEvent = new CloudEvent
        {
            Id = _idProvider.ComputeCloudEventId(domainEvent.EventTypeId, domainEvent.OccurredAt),
            Source = new Uri("urn:offgrid:customers:outboxworker"),
            Type = domainEvent.EventType,
            Time = domainEvent.OccurredAt,
            Subject = $"customers/{domainEvent.AggregateId}",
            DataContentType = "application/json",
            Data = domainEvent
        };

        cloudEvent["correlationid"] = domainEvent.CorrelationId;
        cloudEvent["eventtypeid"] = domainEvent.EventTypeId;

        return cloudEvent;
    }
}
