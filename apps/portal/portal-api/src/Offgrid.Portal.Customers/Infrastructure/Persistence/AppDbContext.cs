using Microsoft.EntityFrameworkCore;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Infrastructure.Persistence.EntityConfigurations;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public AppDbContext(
        DbContextOptions<AppDbContext> dbContext,
        IDomainEventDispatcher domainEventDispatcher) : base(dbContext)
    {
        ArgumentNullException.ThrowIfNull(domainEventDispatcher, nameof(domainEventDispatcher));
        _domainEventDispatcher = domainEventDispatcher;
    }

    public DbSet<Customer> Customers => Set<Customer>();

    public bool HasChanges()
    {
        return ChangeTracker.HasChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 1. Collect domain events before saving changes
        // This ensures we capture events from entities being tracked
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Count == 0)
        {
            return result;
        }

        // 2. Dispatch domain events
        await _domainEventDispatcher.DispatchEventsAsync(domainEvents, cancellationToken);

        // 3. Clear domain events after successful commit
        foreach (var entity in ChangeTracker.Entries<AggregateRoot>())
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}
