using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Infrastructure.Persistence.EntityConfigurations;

public class CustomerChangeConfiguration : IEntityTypeConfiguration<CustomerChange>
{
    public void Configure(EntityTypeBuilder<CustomerChange> entity)
    {
        var jsonOptions = JsonSerializationOptions.Default;
        var changesConverter = new ValueConverter<List<Change>, string>(
            value => JsonSerializer.Serialize(value ?? new List<Change>(), jsonOptions),
            value => JsonSerializer.Deserialize<List<Change>>(value, jsonOptions) ?? new List<Change>());

        var changesComparer = new ValueComparer<List<Change>>(
            (left, right) => JsonSerializer.Serialize(left ?? new List<Change>(), jsonOptions) ==
                JsonSerializer.Serialize(right ?? new List<Change>(), jsonOptions),
            value => JsonSerializer.Serialize(value ?? new List<Change>(), jsonOptions).GetHashCode(),
            value => JsonSerializer.Deserialize<List<Change>>(
                JsonSerializer.Serialize(value ?? new List<Change>(), jsonOptions),
                jsonOptions) ?? new List<Change>());

        // map table
        entity.ToTable(name: "customer_change", schema: Schema.CUSTOMERS);

        // map primary key
        entity.HasKey(e => e.CustomerChangeId);
        entity.Property(e => e.CustomerChangeId).HasColumnName("customer_change_id").IsRequired();

        // // map columns
        entity.Property(e => e.CreatedDate).HasColumnName("created_date").IsRequired();
        entity.Property(e => e.CustomerId).HasColumnName("customer_id").IsRequired();
        entity.Property(e => e.ChangedBy).HasColumnName("changed_by").HasMaxLength(256).IsRequired();
        entity.Property(e => e.ChangedAt).HasColumnName("changed_at").IsRequired();
        var changesProperty = entity.Property(e => e.Changes)
            .HasColumnName("changes")
            .HasColumnType("jsonb")
            .IsRequired();
        changesProperty.HasConversion(changesConverter);
        changesProperty.Metadata.SetValueComparer(changesComparer);

        // indexes
        entity.HasIndex(e => e.ChangedBy).HasDatabaseName("ix_customers_customerchange_changedby");
        entity.HasIndex(e => e.CustomerId).HasDatabaseName("ix_customers_customerchange_customerid");
    }
}
