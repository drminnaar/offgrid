using Offgrid.Customers.Domain.Entities;

namespace Offgrid.Customers.Domain.Services;

public class CustomerFactory : ICustomerFactory
{
    private readonly TimeProvider _timeProvider;
    private readonly ICustomerIdGenerator _idGenerator;
    private readonly ICustomerNumberGenerator _numberGenerator;

    public CustomerFactory(TimeProvider timeProvider, ICustomerIdGenerator idGenerator, ICustomerNumberGenerator numberGenerator)
    {
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        _numberGenerator = numberGenerator ?? throw new ArgumentNullException(nameof(numberGenerator));
    }

    public Customer Create(string keycloakUserId, string email, string fullName)
        => Customer.Create(keycloakUserId, email, fullName, _timeProvider, _idGenerator, _numberGenerator);
}
