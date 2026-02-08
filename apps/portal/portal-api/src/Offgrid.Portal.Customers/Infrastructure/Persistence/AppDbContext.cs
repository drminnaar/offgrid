using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Infrastructure.Persistence.EntityConfigurations;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerChange> CustomerChanges => Set<CustomerChange>();
    public DbSet<CustomerOutbox> CustomerOutboxes => Set<CustomerOutbox>();

    public bool HasChanges()
    {
        return ChangeTracker.HasChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
    }
}
