using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Domain.Repositories;

public interface ICustomerChangeRepository
{
    void Add(CustomerChange customerChange);
}
