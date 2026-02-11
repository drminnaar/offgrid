using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Offgrid.Shop.Customers.Domain.Entities;

namespace Offgrid.Shop.Customers.Infrastructure.Persistence.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        // map table
        entity.ToTable(name: "customer", schema: Schema.CUSTOMERS);

        // map primary key
        entity.HasKey(e => e.CustomerId);
        entity.Property(e => e.CustomerId).HasColumnName("customer_id").IsRequired();

        // // map columns
        entity.Property(e => e.CreatedDate).HasColumnName("created_date").IsRequired();
        entity.Property(e => e.DeletedDate).HasColumnName("deleted_date");
        entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(256).IsRequired();
        entity.Property(e => e.KeycloakUserId).HasColumnName("keycloak_user_id").IsRequired();
        entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(256).IsRequired();
        entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(256).IsRequired();
        entity
            .Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<CustomerStatus>(v));
        entity.Property(e => e.CustomerNumber).HasColumnName("customer_number").HasMaxLength(20).IsRequired();
        entity.Property(e => e.Version).HasColumnName("version").IsRowVersion().IsRequired();

        // unique indexes
        entity.HasIndex(e => e.KeycloakUserId).IsUnique().HasDatabaseName("ux_customers_customer_keycloakuserid");
        entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("ux_customers_customer_email");
        entity.HasIndex(e => e.CustomerNumber).IsUnique().HasDatabaseName("ux_customers_customer_customernumber");

        // indexes
        entity.HasIndex(e => e.Status).HasDatabaseName("ix_customers_customer_status");
    }
}
