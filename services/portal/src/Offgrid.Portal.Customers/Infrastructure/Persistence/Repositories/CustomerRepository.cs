using Microsoft.EntityFrameworkCore;
using Offgrid.Framework.EntityFrameworkCore.Extensions;
using Offgrid.Framework.System.Collections.Generic;
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

    public async Task<IPagedList<Customer>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Customers
            .AsNoTracking()
            .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CustomerId == id, cancellationToken);
    }

    public void Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
    }
}
