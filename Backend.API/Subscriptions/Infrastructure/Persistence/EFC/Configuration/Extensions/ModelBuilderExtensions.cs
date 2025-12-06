using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model Builder Extensions for Subscriptions Context
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        // Configuración de tabla
        builder.Entity<Subscription>().ToTable("subscriptions");
        
        builder.Entity<Subscription>().HasKey(s => s.Id);
        builder.Entity<Subscription>().Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Subscription>().Ignore(s => s.Period);

        builder.Entity<Subscription>()
            .Property("_startDate")
            .HasColumnName("StartDate")
            .IsRequired();

        builder.Entity<Subscription>()
            .Property("_endDate")
            .HasColumnName("EndDate")
            .IsRequired();

        builder.Entity<Subscription>()
            .Property(s => s.Price)
            .HasConversion(
                v => v.Amount,
                v => new SubscriptionPrice(v, "USD"))
            .HasColumnName("Amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Currency como propiedad shadow
        builder.Entity<Subscription>()
            .Property<string>("Currency")
            .HasColumnName("Currency")
            .HasMaxLength(3)
            .HasDefaultValue("USD")
            .IsRequired();

        builder.Entity<Subscription>()
            .Property(s => s.PaymentReference)
            .HasConversion(
                v => v.Value,
                v => new PaymentReference(v))
            .HasColumnName("PaymentReference")
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<Subscription>()
            .Property(s => s.Status)
            .HasConversion(
                v => v.Value,
                v => SubscriptionStatus.FromString(v))
            .HasColumnName("Status")
            .HasMaxLength(20)
            .IsRequired();

        builder.Entity<Subscription>()
            .Property(s => s.BillingCycle)
            .HasConversion(
                v => v.Months,
                v => BillingCycle.Custom(v))
            .HasColumnName("BillingCycleMonths")
            .IsRequired();

        // Propiedades simples
        builder.Entity<Subscription>()
            .Property(s => s.PlanId)
            .HasColumnName("PlanId")
            .IsRequired();
        
        builder.Entity<Subscription>()
            .Property(s => s.UserId)
            .HasColumnName("UserId")
            .IsRequired();
        
        builder.Entity<Subscription>()
            .Property(s => s.CreatedDate)
            .HasColumnName("CreatedAt");
        
        builder.Entity<Subscription>()
            .Property(s => s.UpdatedDate)
            .HasColumnName("UpdatedAt");

        // Índices
        builder.Entity<Subscription>().HasIndex(s => s.UserId);
        builder.Entity<Subscription>().HasIndex(s => s.PlanId);
    }
}
