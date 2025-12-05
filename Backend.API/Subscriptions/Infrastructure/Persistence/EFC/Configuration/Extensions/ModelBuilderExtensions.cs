using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model Builder Extensions for Subscriptions Context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the subscriptions configuration
    /// </summary>
    /// <param name="builder">The model builder</param>
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        // Subscription Aggregate
        builder.Entity<Subscription>().HasKey(s => s.Id);
        builder.Entity<Subscription>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Subscription>().Property(s => s.UserId).IsRequired().HasColumnName("UserId");
        builder.Entity<Subscription>().Property(s => s.PlanType).IsRequired().HasMaxLength(20).HasColumnName("PlanType");
        builder.Entity<Subscription>().Property(s => s.StartDate).IsRequired().HasColumnName("StartDate");
        builder.Entity<Subscription>().Property(s => s.EndDate).HasColumnName("EndDate");
        builder.Entity<Subscription>().Property(s => s.MaxMembers).IsRequired().HasColumnName("MaxMembers");
        builder.Entity<Subscription>().Property(s => s.MaxInventoryItems).IsRequired().HasColumnName("MaxInventoryItems");
        builder.Entity<Subscription>().Property(s => s.IsActive).IsRequired().HasColumnName("IsActive");
        builder.Entity<Subscription>().Property(s => s.CreatedDate).HasColumnName("CreatedAt");
        builder.Entity<Subscription>().Property(s => s.UpdatedDate).HasColumnName("UpdatedAt");

        // Subscription Indexes
        builder.Entity<Subscription>().HasIndex(s => s.UserId);
        builder.Entity<Subscription>().HasIndex(s => s.IsActive);
        builder.Entity<Subscription>().HasIndex(s => new { s.UserId, s.IsActive });

        // SubscriptionPlan Aggregate
        builder.Entity<SubscriptionPlan>().HasKey(p => p.Id);
        builder.Entity<SubscriptionPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SubscriptionPlan>().Property(p => p.Name).IsRequired().HasMaxLength(20).HasColumnName("Name");
        builder.Entity<SubscriptionPlan>().Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)").HasColumnName("Price");
        builder.Entity<SubscriptionPlan>().Property(p => p.Currency).IsRequired().HasMaxLength(10).HasColumnName("Currency");
        builder.Entity<SubscriptionPlan>().Property(p => p.Period).IsRequired().HasMaxLength(20).HasColumnName("Period");
        builder.Entity<SubscriptionPlan>().Property(p => p.MaxMembers).IsRequired().HasColumnName("MaxMembers");
        builder.Entity<SubscriptionPlan>().Property(p => p.MaxInventoryItems).IsRequired().HasColumnName("MaxInventoryItems");
        builder.Entity<SubscriptionPlan>().Property(p => p.Features).IsRequired().HasColumnType("json").HasColumnName("Features");
        builder.Entity<SubscriptionPlan>().Property(p => p.CreatedDate).HasColumnName("CreatedAt");
        builder.Entity<SubscriptionPlan>().Property(p => p.UpdatedDate).HasColumnName("UpdatedAt");

        // SubscriptionPlan Indexes
        builder.Entity<SubscriptionPlan>().HasIndex(p => p.Name).IsUnique();

        // Seed Data
        builder.Entity<SubscriptionPlan>().HasData(
            new
            {
                Id = 1,
                Name = "Free",
                Price = 0m,
                Currency = "USD",
                Period = "monthly",
                MaxMembers = 3,
                MaxInventoryItems = 50,
                Features = "[\"Gestión básica de inventario\",\"Hasta 3 miembros por laboratorio\",\"Hasta 50 ítems de inventario\"]"
            },
            new
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
            new
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
