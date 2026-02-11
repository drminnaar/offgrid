using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<CustomerChange> CustomerChanges { get; }
    DbSet<CustomerOutboxMessage> CustomerOutboxMessages { get; }

    bool HasChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
