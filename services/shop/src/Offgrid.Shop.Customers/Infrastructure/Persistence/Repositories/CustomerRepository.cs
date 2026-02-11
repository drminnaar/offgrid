using Microsoft.EntityFrameworkCore;
using Offgrid.Shop.Customers.Domain.Entities;
using Offgrid.Shop.Customers.Domain.Repositories;

namespace Offgrid.Shop.Customers.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IAppDbContext _dbContext;

    public CustomerRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CustomerId == id, cancellationToken);
    }

    public Task<Customer?> GetByKeycloakUserIdAsync(string keycloakUserId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.KeycloakUserId == keycloakUserId, cancellationToken);
    }

    public Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _dbContext.Customers.Add(customer);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _dbContext.Customers.Update(customer);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
