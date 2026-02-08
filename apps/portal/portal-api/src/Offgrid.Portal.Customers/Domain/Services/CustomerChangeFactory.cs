using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Domain.Services;

public interface ICustomerChangeFactory
{
    CustomerChange Create(
        Guid customerId,
        string changedBy,
        IReadOnlyCollection<Change> changes,
        DateTimeOffset changedDate);
}

public class CustomerChangeFactory : ICustomerChangeFactory
{
    private readonly TimeProvider _timeProvider;
    private readonly IEntityIdGenerator _idGenerator;

    public CustomerChangeFactory(TimeProvider timeProvider, IEntityIdGenerator idGenerator)
    {
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
    }

    public CustomerChange Create(Guid customerId, string changedBy, IReadOnlyCollection<Change> changes, DateTimeOffset changedDate)
    {
        ArgumentNullException.ThrowIfNull(changes);
        ArgumentException.ThrowIfNullOrWhiteSpace(changedBy);

        return CustomerChange.CreateNew(
            _idGenerator.GenerateEntityId(),
            customerId,
            changedBy,
            changes,
            changedDate,
            _timeProvider.GetUtcNow()
        );
    }
}
