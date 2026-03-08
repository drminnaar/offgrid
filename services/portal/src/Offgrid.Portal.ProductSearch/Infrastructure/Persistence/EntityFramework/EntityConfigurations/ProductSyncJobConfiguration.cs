using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Offgrid.Portal.ProductSearch.Domain.Entities;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

public sealed class IndexingJobConfiguration : IEntityTypeConfiguration<IndexingJob>
{
    public void Configure(EntityTypeBuilder<IndexingJob> entity)
    {
        // map table
        entity.ToTable(name: "indexing_job", schema: "product_search");

        // map primary key
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id").IsRequired();

        // map columns
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        entity.Property(e => e.IsIndexing).HasColumnName("is_indexing").HasDefaultValue(false).IsRequired();
        entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
        entity.Property(e => e.Error).HasColumnName("error");
        entity.Property(e => e.RetryCount).HasColumnName("retry_count").HasDefaultValue(0).IsRequired();
        entity.Property(e => e.NextRetryAt).HasColumnName("next_retry_at");
        entity.Property(e => e.IsDeadletter).HasColumnName("is_deadletter").HasDefaultValue(false).IsRequired();

        // indexes
        entity
            .HasIndex(e => e.CompletedAt)
            .HasDatabaseName("ix_productsearch_indexingjob_completedat")
            .HasFilter("completed_at IS NULL");
        entity
            .HasIndex(e => e.IsIndexing)
            .HasDatabaseName("ix_productsearch_indexingjob_isindexing")
            .HasFilter("completed_at IS NULL");
        entity
            .HasIndex(e => e.NextRetryAt)
            .HasDatabaseName("ix_productsearch_indexingjob_nextretryat")
            .HasFilter("completed_at IS NULL");
    }
}
