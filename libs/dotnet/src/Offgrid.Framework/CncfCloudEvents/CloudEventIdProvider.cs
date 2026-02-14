using NanoidDotNet;

namespace Offgrid.Framework.CncfCloudEvents;

/// <summary>
/// Provides functionality to generate unique CloudEvent IDs. The generated IDs are based on the
/// event type ID, the time the event occurred, and a random component to ensure uniqueness. The
/// format of the generated ID is: {eventTypeId}-{randomPart}-{occurredAt:yyyyMMddHHmmss}.
/// </summary>
public sealed class CloudEventIdProvider : ICloudEventIdProvider
{
    /// <summary>
    /// The alphabet used for generating the random part of the CloudEvent ID. It includes digits
    /// and uppercase letters, excluding easily confusable characters like 'I', 'L', 'O', and 'U'
    /// to enhance readability.
    /// </summary>
    private const string SafeAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";

    /// <summary>
    /// The length of the random part of the CloudEvent ID. The random part is 8 characters long,
    /// providing a large number of unique combinations (36^8) to ensure uniqueness even for events
    /// of the same type that occur at the same time.
    /// </summary>
    private const int RandomPartLength = 8;

    /// <summary>
    /// Computes a unique CloudEvent ID based on the event type ID and the time the event
    /// occurred. The generated ID includes a random component to ensure uniqueness even
    /// for events of the same type that occur at the same time. The format of the
    /// generated ID is: {eventTypeId}-{randomPart}-{occurredAt:yyyyMMddHHmmss}.
    /// </summary>
    /// <param name="eventTypeId">The ID of the event type.</param>
    /// <param name="occurredAt">The time the event occurred.</param>
    /// <returns>A unique CloudEvent ID.</returns>
    public string ComputeCloudEventId(string eventTypeId, DateTimeOffset occurredAt)
    {
        var randomPart = Nanoid.Generate(alphabet: SafeAlphabet, size: RandomPartLength);
        return $"{eventTypeId}-{randomPart}-{occurredAt:yyyyMMddHHmmss}";
    }
}
