using Offgrid.Framework.Domain;
using Offgrid.Framework.Domain.Extensions;
using Offgrid.Portal.Customers.Domain.Repositories;

namespace Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;

public interface IGetAllCustomersQueryHandler
{
    Task<PagedListResult<CustomerInfo>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken);
}

public class GetAllCustomersQueryHandler : IGetAllCustomersQueryHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        ArgumentNullException.ThrowIfNull(customerRepository, nameof(customerRepository));
        _customerRepository = customerRepository;
    }

    public async Task<PagedListResult<CustomerInfo>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(query.PageNumber, query.PageSize, cancellationToken);
        return customers.ToPagedListResult(c => c.ToCustomerInfo());
    }
}
