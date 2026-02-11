namespace Offgrid.Shop.Customers.Domain.Services;

public sealed class CustomerIdGenerator : ICustomerIdGenerator
{
    public Guid GenerateCustomerId() => Guid.CreateVersion7();
}
