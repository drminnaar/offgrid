using Microsoft.EntityFrameworkCore;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Repositories;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IAppDbContext _dbContext;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerChangeRepository _customerChangeRepository;
    private readonly ICustomerOutboxRepository _customerOutboxRepository;

    public UnitOfWork(
        IAppDbContext dbContext,
        ICustomerRepository customerRepository,
        ICustomerChangeRepository customerChangeRepository,
        ICustomerOutboxRepository customerOutboxRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _customerChangeRepository = customerChangeRepository ??
            throw new ArgumentNullException(nameof(customerChangeRepository));
        _customerOutboxRepository = customerOutboxRepository ??
            throw new ArgumentNullException(nameof(customerOutboxRepository));
    }

    public ICustomerRepository Customers => _customerRepository;
    public ICustomerChangeRepository CustomerChanges => _customerChangeRepository;
    public ICustomerOutboxRepository CustomerOutboxes => _customerOutboxRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!_dbContext.HasChanges())
        {
            return 0;
        }

        int stateEntriesWritten;

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

        return stateEntriesWritten;
    }
}
