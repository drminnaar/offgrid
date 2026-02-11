using CloudNative.CloudEvents;

namespace Offgrid.Portal.Customers.OutboxProcessor.Application.Services;

public interface IEventPublisher
{
    Task PublishAsync(CloudEvent cloudEvent, CancellationToken cancellationToken = default);
}
