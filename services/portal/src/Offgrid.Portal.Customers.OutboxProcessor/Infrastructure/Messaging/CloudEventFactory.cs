using System.Text.Json;
using CloudNative.CloudEvents;
using Offgrid.Framework.CncfCloudEvents;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

public sealed class CloudEventFactory(ICloudEventIdProvider idProvider) : CloudEventFactoryBase<IDomainEvent>(idProvider)
{
    private static readonly IReadOnlyDictionary<string, (Type sourceEventType, string cloudEventType)> _typeMap =
        new Dictionary<string, (Type sourceEventType, string cloudEventType)>(StringComparer.OrdinalIgnoreCase)
        {
            [nameof(CustomerChangedEvent)] = (typeof(CustomerChangedEvent), "com.offgrid.portal.customers.customer-changed"),
            [nameof(CustomerReinstatedEvent)] = (typeof(CustomerReinstatedEvent), "com.offgrid.portal.customers.customer-reinstated"),
            [nameof(CustomerSuspendedEvent)] = (typeof(CustomerSuspendedEvent), "com.offgrid.portal.customers.customer-suspended"),
        };

    protected override IReadOnlyDictionary<string, (Type sourceEventType, string cloudEventType)> EventTypeMap => _typeMap;

    protected override CloudEvent CreateCloudEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));

        var eventTypeName = domainEvent.GetType().Name;
        if (!EventTypeMap.TryGetValue(eventTypeName, out var eventMap))
        {
            throw new InvalidOperationException($"Unsupported domain event type: {eventTypeName}");
        }

        var cloudEvent = new CloudEvent
        {
            Id = IdProvider.ComputeCloudEventId(domainEvent.EventTypeId, domainEvent.OccurredAt),
            Source = new Uri("urn:offgrid:customers:outboxworker", UriKind.Absolute),
            Type = eventMap.cloudEventType,
            Time = domainEvent.OccurredAt,
            Subject = $"customers/{domainEvent.AggregateId}",
            DataContentType = "application/json",
            Data = JsonSerializer.SerializeToUtf8Bytes(domainEvent, eventMap.sourceEventType, JsonOptions)
        };

        cloudEvent["correlationid"] = domainEvent.CorrelationId;

        return cloudEvent;
    }
}
