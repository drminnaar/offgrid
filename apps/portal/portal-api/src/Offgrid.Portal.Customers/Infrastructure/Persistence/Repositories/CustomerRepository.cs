using Microsoft.EntityFrameworkCore;
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

    public void Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
    }
}
