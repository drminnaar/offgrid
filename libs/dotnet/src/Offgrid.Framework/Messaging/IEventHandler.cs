namespace Offgrid.Framework.Messaging;

/// <summary>
/// Interface for event handlers that process events of type TEvent. Implementations should
/// return true if the event was handled successfully, or false to reject and requeue the event.
/// </summary>
public interface IEventHandler<in TEvent> where TEvent : class
{
    /// <summary>
    /// Handles the incoming event. Return true if the event was handled successfully, false to
    /// reject and requeue.
    /// </summary>
    /// <param name="event">The event to handle.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if event was handled successfully, false to reject and requeue</returns>
    Task<bool> HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
