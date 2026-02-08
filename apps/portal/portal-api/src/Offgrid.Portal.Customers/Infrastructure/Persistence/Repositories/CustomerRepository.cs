using Microsoft.EntityFrameworkCore;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

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
        return _dbContext
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CustomerId == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.HasChanges())
        {
            var stateEntriesWritten = 0;

            try
            {
                stateEntriesWritten = await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DomainException("Customer was modified by another user. Please try update again.",
                    [new("customer", ["Concurrency conflict - customer was updated by another process"])]);
            }

            if (stateEntriesWritten == 0)
            {
                throw new DomainException(
                    "Failed to save customer details to the database. No state entries were written.",
                    [new("customer", ["No state entries were written"])]);
            }
        }
    }

    public void Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
    }
}
