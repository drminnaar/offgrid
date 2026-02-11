namespace Offgrid.Portal.Customers.Domain.Repositories;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }
    ICustomerChangeRepository CustomerChanges { get; }
    ICustomerOutboxRepository CustomerOutboxes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
