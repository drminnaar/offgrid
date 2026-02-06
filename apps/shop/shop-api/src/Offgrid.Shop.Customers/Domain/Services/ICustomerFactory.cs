using Offgrid.Shop.Customers.Domain.Entities;

namespace Offgrid.Shop.Customers.Domain.Services;

public interface ICustomerFactory
{
    Customer Create(string keycloakUserId, string email, string fullName);
}
