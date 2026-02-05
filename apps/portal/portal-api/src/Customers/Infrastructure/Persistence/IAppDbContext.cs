using Microsoft.EntityFrameworkCore;
using Offgrid.Customers.Domain.Entities;

namespace Offgrid.Customers.Infrastructure.Persistence;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
