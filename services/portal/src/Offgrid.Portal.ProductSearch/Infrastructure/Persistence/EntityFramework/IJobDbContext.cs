using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.ProductSearch.Domain.Entities;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework;

/// <summary>
/// Defines the contract for the application's database context, which is responsible for managing
/// the entity sets and providing methods for saving changes to the database. This interface
/// abstracts the underlying database implementation, allowing for easier testing and separation of
/// concerns within the application.
/// </summary>
public interface IJobDbContext
{
    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for <see cref="IndexingJob"/> entities.
    /// </summary>
    /// <value></value>
    DbSet<IndexingJob> IndexingJobs { get; }

    /// <summary>
    /// Determines whether there are any changes in the tracked entities that need to be persisted to the database.
    /// </summary>
    /// <returns><c>true</c> if there are changes to be saved; otherwise, <c>false</c>.</returns>
    bool HasChanges();

    /// <summary>
    /// Saves all changes made in the context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The number of state entries written to the underlying database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
