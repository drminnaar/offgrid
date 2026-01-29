namespace Offgrid.Customers.Domain.Services;

public sealed class CustomerIdGenerator : ICustomerIdGenerator
{
    public Guid GenerateCustomerId() => Guid.CreateVersion7();
}
