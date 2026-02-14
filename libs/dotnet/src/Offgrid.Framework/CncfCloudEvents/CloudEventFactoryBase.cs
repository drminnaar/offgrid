using System.Text.Json;
using CloudNative.CloudEvents;
using Offgrid.Framework.System.Text;

namespace Offgrid.Framework.CncfCloudEvents;

public abstract class CloudEventFactoryBase<TSourceEvent>
{
    private readonly ICloudEventIdProvider _idProvider;

    private readonly JsonSerializerOptions _jsonOptions = JsonSerializationOptions.Messaging;

    protected CloudEventFactoryBase(ICloudEventIdProvider cloudEventIdProvider)
    {
        ArgumentNullException.ThrowIfNull(cloudEventIdProvider, nameof(cloudEventIdProvider));
        _idProvider = cloudEventIdProvider;
    }

    protected abstract IReadOnlyDictionary<string, (Type sourceEventType, string cloudEventType)> EventTypeMap { get; }

    protected ICloudEventIdProvider IdProvider => _idProvider;
    protected JsonSerializerOptions JsonOptions => _jsonOptions;

    public CloudEvent CreateCloudEvent(string sourceEventType, string sourceEventJson)
    {
        if (!EventTypeMap.TryGetValue(sourceEventType, out var eventInfo))
        {
            throw new InvalidOperationException($"Unsupported source event type: {sourceEventType}");
        }

        if (JsonSerializer.Deserialize(sourceEventJson, eventInfo.sourceEventType, _jsonOptions) is not TSourceEvent deserializedEvent)
        {
            throw new InvalidOperationException($"Failed to deserialize source event of type {sourceEventType}");
        }

        return CreateCloudEvent(deserializedEvent);
    }

    protected abstract CloudEvent CreateCloudEvent(TSourceEvent sourceEvent);
}
