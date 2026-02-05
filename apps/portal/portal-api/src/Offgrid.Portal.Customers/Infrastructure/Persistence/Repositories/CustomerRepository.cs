using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;
using Offgrid.Portal.Customers.Infrastructure.Persistence;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly IAppDbContext _dbContext;

    public CustomerRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var stateEntriesWritten = await _dbContext.SaveChangesAsync(cancellationToken);
        if (stateEntriesWritten == 0)
        {
            throw new DbUpdateException("Failed to save customer changes to the database. No state entries were written.");
        }
    }
}
