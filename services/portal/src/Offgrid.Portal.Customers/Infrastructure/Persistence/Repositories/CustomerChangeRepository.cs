using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence.Repositories;

public sealed class CustomerChangeRepository : ICustomerChangeRepository
{
    private readonly IAppDbContext _dbContext;

    public CustomerChangeRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void Add(CustomerChange customerChange)
    {
        ArgumentNullException.ThrowIfNull(customerChange, nameof(customerChange));
        _dbContext.CustomerChanges.Add(customerChange);
    }
}
