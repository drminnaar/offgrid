using System.Text.Json;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Domain.Services;

public interface ICustomerOutboxFactory
{
    CustomerOutboxMessage Create(IDomainEvent domainEvent);
}

public class CustomerOutboxFactory : ICustomerOutboxFactory
{
    private readonly TimeProvider _timeProvider;
    private readonly IEntityIdGenerator _idGenerator;

    public CustomerOutboxFactory(TimeProvider timeProvider, IEntityIdGenerator idGenerator)
    {
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
    }

    public CustomerOutboxMessage Create(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        var payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());

        return CustomerOutboxMessage.CreateNew(
            _idGenerator.GenerateEntityId(),
            domainEvent.EventTypeId,
            domainEvent.EventType,
            payload,
            domainEvent.OccurredAt,
            _timeProvider.GetUtcNow()
        );
    }
}
