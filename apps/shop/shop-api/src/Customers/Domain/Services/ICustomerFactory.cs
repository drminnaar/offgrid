using Offgrid.Customers.Domain.Entities;

namespace Offgrid.Customers.Domain.Services;

public interface ICustomerFactory
{
    Customer Create(string keycloakUserId, string email, string fullName);
}
