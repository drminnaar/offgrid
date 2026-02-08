using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Domain.Repositories;

public interface ICustomerOutboxRepository
{
    void Add(CustomerOutbox customerOutbox);
}
