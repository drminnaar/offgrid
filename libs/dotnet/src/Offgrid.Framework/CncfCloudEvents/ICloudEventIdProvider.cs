namespace Offgrid.Framework.CncfCloudEvents;

/// <summary>
/// Defines a contract for providing unique CloudEvent IDs. Implementations of this interface
/// are responsible for generating CloudEvent IDs that are unique and can be used to identify
/// individual events. The generated IDs typically include components such as the event type ID,
/// the time the event occurred, and a random component to ensure uniqueness even for events of
/// the same type that occur at the same time.
/// </summary>
public interface ICloudEventIdProvider
{
    /// <summary>
    /// Computes a unique CloudEvent ID based on the event type ID and the time the event
    /// occurred. The generated ID includes a random component to ensure uniqueness even
    /// for events of the same type that occur at the same time.
    /// </summary>
    /// <param name="eventTypeId">The ID of the event type.</param>
    /// <param name="occurredAt">The time the event occurred.</param>
    /// <returns>A unique CloudEvent ID.</returns>
    string ComputeCloudEventId(string eventTypeId, DateTimeOffset occurredAt);
}
