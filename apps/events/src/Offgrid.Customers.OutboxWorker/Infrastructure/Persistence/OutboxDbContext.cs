using Microsoft.EntityFrameworkCore;
using Offgrid.Customers.OutboxWorker.Domain.Entities;
using Offgrid.Customers.OutboxWorker.Infrastructure.Persistence.EntityConfigurations;

namespace Offgrid.Customers.OutboxWorker.Infrastructure.Persistence;

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
