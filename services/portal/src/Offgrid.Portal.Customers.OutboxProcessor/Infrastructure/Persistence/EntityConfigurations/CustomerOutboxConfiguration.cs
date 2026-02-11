using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Offgrid.Portal.Customers.OutboxProcessor.Domain.Entities;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence.EntityConfigurations;

public sealed class CustomerOutboxMessageConfiguration : IEntityTypeConfiguration<CustomerOutboxMessage>
{
    public void Configure(EntityTypeBuilder<CustomerOutboxMessage> entity)
    {
        entity.ToTable(name: "customer_outbox_message", schema: Schema.CUSTOMERS);

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id").IsRequired();

        entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        entity.Property(e => e.EventTypeId).HasColumnName("event_type_id").HasMaxLength(255).IsRequired();
        entity.Property(e => e.EventType).HasColumnName("event_type").HasMaxLength(255).IsRequired();
        entity.Property(e => e.Payload).HasColumnName("payload").HasColumnType("jsonb").IsRequired();
        entity.Property(e => e.OccurredAt).HasColumnName("occurred_at").IsRequired();
        entity.Property(e => e.ProcessedAt).HasColumnName("processed_at");
        entity.Property(e => e.Error).HasColumnName("error");
        entity.Property(e => e.RetryCount).HasColumnName("retry_count").HasDefaultValue(0).IsRequired();
        entity.Property(e => e.NextRetryAt).HasColumnName("next_retry_at");
        entity.Property(e => e.IsDeadletter).HasColumnName("is_deadletter").HasDefaultValue(false).IsRequired();

        entity
            .HasIndex(e => e.OccurredAt)
            .HasDatabaseName("ix_customers_customeroutboxmessage_occurredat")
            .HasFilter("processed_at IS NULL");
        entity
            .HasIndex(e => e.NextRetryAt)
            .HasDatabaseName("ix_customers_customeroutboxmessage_nextretryat")
            .HasFilter("processed_at IS NULL");
    }
}
