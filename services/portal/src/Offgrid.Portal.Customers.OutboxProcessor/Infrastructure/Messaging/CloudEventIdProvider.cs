using NanoidDotNet;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

public interface ICloudEventIdProvider
{
    string ComputeCloudEventId(string eventTypeId, DateTimeOffset occurredAt);
}

public sealed class CloudEventIdProvider : ICloudEventIdProvider
{
    private const string SafeAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    private const int RandomPartLength = 8;

    public string ComputeCloudEventId(string eventTypeId, DateTimeOffset occurredAt)
    {
        var randomPart = Nanoid.Generate(alphabet: SafeAlphabet, size: RandomPartLength);
        return $"{eventTypeId}-{randomPart}-{occurredAt:yyyyMMddHHmmss}";
    }
}
