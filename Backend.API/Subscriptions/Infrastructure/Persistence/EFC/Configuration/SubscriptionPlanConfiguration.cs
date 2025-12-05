using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.API.Subscriptions.Domain.Model.Aggregates;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.ToTable("subscription_plans");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseMySqlIdentityColumn();
        
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        
        builder.Property(p => p.Currency)
            .HasColumnName("currency")
            .HasMaxLength(10)
            .IsRequired();
        
        builder.Property(p => p.Period)
            .HasColumnName("period")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(p => p.MaxMembers)
            .HasColumnName("max_members")
            .IsRequired();
        
        builder.Property(p => p.MaxInventoryItems)
            .HasColumnName("max_inventory_items")
            .IsRequired();
        
        builder.Property(p => p.Features)
            .HasColumnName("features")
            .HasColumnType("json")
            .IsRequired();

        builder.HasIndex(p => p.Name)
            .IsUnique()
            .HasDatabaseName("idx_subscription_plans_name_unique");

        builder.HasData(
            new SubscriptionPlan
            {
                Id = 1,
                Name = "Free",
                Price = 0,
                Currency = "USD",
                Period = "monthly",
                MaxMembers = 3,
                MaxInventoryItems = 50,
                Features = "[\"Gestión básica de inventario\",\"Hasta 3 miembros por laboratorio\",\"Hasta 50 ítems de inventario\"]"
            },
            new SubscriptionPlan
            {
                Id = 2,
                Name = "Pro",
                Price = 29.99m,
                Currency = "USD",
                Period = "monthly",
                MaxMembers = 10,
                MaxInventoryItems = 500,
                Features = "[\"Gestión avanzada de inventario\",\"Hasta 10 miembros por laboratorio\",\"Hasta 500 ítems de inventario\",\"Alertas automáticas\",\"Reportes básicos\"]"
            },
            new SubscriptionPlan
            {
                Id = 3,
                Name = "Max",
                Price = 99.99m,
                Currency = "USD",
                Period = "monthly",
                MaxMembers = -1,
                MaxInventoryItems = -1,
                Features = "[\"Todo en Pro\",\"Miembros ilimitados\",\"Ítems ilimitados\",\"Integración IoT completa\",\"Reportes avanzados\",\"Soporte prioritario\"]"
            }
        );
    }
}
