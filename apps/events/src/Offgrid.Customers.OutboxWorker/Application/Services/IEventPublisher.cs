using CloudNative.CloudEvents;

namespace Offgrid.Customers.OutboxWorker.Application.Services;

public interface IEventPublisher
{
    Task PublishAsync(CloudEvent cloudEvent, CancellationToken cancellationToken = default);
}
