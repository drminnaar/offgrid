using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

namespace Offgrid.Portal.Customers.Application.Queries.GetCustomerById;

public interface IGetCustomerByIdQueryHandler
{
    Task<CustomerDetail> Handle(Guid customerId, CancellationToken cancellationToken);
}

public class GetCustomerByIdQueryHandler : IGetCustomerByIdQueryHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        ArgumentNullException.ThrowIfNull(customerRepository, nameof(customerRepository));
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDetail> Handle(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository
            .GetByIdAsync(customerId, cancellationToken)
            ?? throw new EntityNotFoundException($"Customer with ID {customerId} not found.")
            {
                EntityKey = customerId.ToString(),
                EntityType = nameof(Customer)
            };

        return customer.ToCustomerDetail();
    }
}
