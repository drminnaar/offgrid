using Microsoft.EntityFrameworkCore;
using Offgrid.Customers.Domain.Entities;
using Offgrid.Customers.Infrastructure.Persistence.EntityConfigurations;

namespace Offgrid.Customers.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
    }
}
