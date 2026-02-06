using Microsoft.EntityFrameworkCore;
using Offgrid.Shop.Customers.Domain.Entities;

namespace Offgrid.Shop.Customers.Infrastructure.Persistence;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
