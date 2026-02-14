namespace Offgrid.Framework.Messaging;

public interface IEventPublisher<TEvent>
{
    Task PublishAsync(TEvent @event, CancellationToken cancellationToken = default);
}
