using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.Customers.OutboxProcessor.Domain.Entities;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence.EntityConfigurations;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence;

public interface IOutboxDbContext
{
    DbSet<CustomerOutboxMessage> CustomerOutboxMessages { get; }
    bool HasChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class OutboxDbContext : DbContext, IOutboxDbContext
{
    public OutboxDbContext(DbContextOptions<OutboxDbContext> options) : base(options)
    {
    }

    public DbSet<CustomerOutboxMessage> CustomerOutboxMessages => Set<CustomerOutboxMessage>();

    public bool HasChanges()
    {
        return ChangeTracker.HasChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerOutboxMessageConfiguration).Assembly);
    }
}
