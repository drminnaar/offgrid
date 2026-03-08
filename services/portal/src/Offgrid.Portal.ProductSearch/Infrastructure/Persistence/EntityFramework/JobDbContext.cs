using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework;

public sealed class JobDbContext : DbContext, IJobDbContext
{
    public JobDbContext(DbContextOptions<JobDbContext> dbContext) : base(dbContext)
    {
    }

    public DbSet<IndexingJob> IndexingJobs => Set<IndexingJob>();

    public bool HasChanges()
    {
        return ChangeTracker.HasChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IndexingJobConfiguration).Assembly);
    }
}
