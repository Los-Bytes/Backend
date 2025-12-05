using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.API.Subscriptions.Domain.Model.Aggregates;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseMySqlIdentityColumn();
        
        builder.Property(s => s.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(s => s.PlanType)
            .HasColumnName("plan_type")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(s => s.StartDate)
            .HasColumnName("start_date")
            .IsRequired();
        
        builder.Property(s => s.EndDate)
            .HasColumnName("end_date");
        
        builder.Property(s => s.MaxMembers)
            .HasColumnName("max_members")
            .IsRequired();
        
        builder.Property(s => s.MaxInventoryItems)
            .HasColumnName("max_inventory_items")
            .IsRequired();
        
        builder.Property(s => s.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
        
        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(s => s.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(s => s.UserId)
            .HasDatabaseName("idx_subscriptions_user_id");
        
        builder.HasIndex(s => s.IsActive)
            .HasDatabaseName("idx_subscriptions_is_active");
        
        builder.HasIndex(s => new { s.UserId, s.IsActive })
            .HasDatabaseName("idx_subscriptions_user_id_is_active");
    }
}
