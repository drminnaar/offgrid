using System.Linq.Expressions;
using Offgrid.Framework.System.Collections.Generic;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IPagedList<Customer>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<IPagedList<Customer>> GetAllAsync(Expression<Func<Customer, bool>> filter, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    void Update(Customer customer);
}
