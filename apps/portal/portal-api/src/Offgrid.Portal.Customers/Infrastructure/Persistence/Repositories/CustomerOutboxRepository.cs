using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence.Repositories;

public sealed class CustomerOutboxRepository : ICustomerOutboxRepository
{
    private readonly IAppDbContext _dbContext;

    public CustomerOutboxRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void Add(CustomerOutboxMessage customerOutboxMessage)
    {
        ArgumentNullException.ThrowIfNull(customerOutboxMessage, nameof(customerOutboxMessage));
        _dbContext.CustomerOutboxMessages.Add(customerOutboxMessage);
    }
}
