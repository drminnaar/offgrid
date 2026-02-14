using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence.EntityConfigurations;

public class CustomerChangeConfiguration : IEntityTypeConfiguration<CustomerChange>
{
    private readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);

    public void Configure(EntityTypeBuilder<CustomerChange> entity)
    {
        // map table
        entity.ToTable(name: "customer_change", schema: Schema.CUSTOMERS);

        // map primary key
        entity.HasKey(e => e.CustomerChangeId);
        entity.Property(e => e.CustomerChangeId).HasColumnName("customer_change_id").IsRequired();

        // // map columns
        entity.Property(e => e.CreatedDate).HasColumnName("created_date").IsRequired();
        entity.Property(e => e.CustomerId).HasColumnName("customer_id").IsRequired();
        entity.Property(e => e.ChangedBy).HasColumnName("changed_by").HasMaxLength(255).IsRequired();
        entity.Property(e => e.ChangedAt).HasColumnName("changed_at").IsRequired();
        var changesProperty = entity.Property(e => e.Changes)
            .HasColumnName("changes")
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonOptions),
                v => JsonSerializer.Deserialize<List<Change>>(v, JsonOptions) ?? new List<Change>())
            .IsRequired();

        // indexes
        entity.HasIndex(e => e.ChangedBy).HasDatabaseName("ix_customers_customerchange_changedby");
        entity.HasIndex(e => e.CustomerId).HasDatabaseName("ix_customers_customerchange_customerid");
    }
}
