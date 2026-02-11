namespace Offgrid.Shop.Customers.Domain.Services;

public interface ICustomerIdGenerator
{
    Guid GenerateCustomerId();
}
