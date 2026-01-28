namespace Offgrid.Customers.Domain.Services;

public interface ICustomerIdGenerator
{
    Guid GenerateCustomerId();
}
