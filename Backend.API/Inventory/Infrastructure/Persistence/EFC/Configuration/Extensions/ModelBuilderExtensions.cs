using Backend.API.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Inventory.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model Builder Extensions for Inventory Context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the inventory context configuration
    /// </summary>
    /// <param name="builder">The model builder</param>
    public static void ApplyInventoryConfiguration(this ModelBuilder builder)
    {
        // Inventory Context

        builder.Entity<Asset>().HasKey(a => a.Id);
        builder.Entity<Asset>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();

        // RfidIdentifier Value Object
        builder.Entity<Asset>().OwnsOne(a => a.RfidTag,
            rfid =>
            {
                rfid.WithOwner().HasForeignKey("Id");
                rfid.Property(r => r.TagId).HasColumnName("RfidTagId");
            });

        // AssetCondition Value Object
        builder.Entity<Asset>().OwnsOne(a => a.AssetCondition,
            condition =>
            {
                condition.WithOwner().HasForeignKey("Id");
                condition.Property(c => c.Temperature).HasColumnName("Temperature");
                condition.Property(c => c.Humidity).HasColumnName("Humidity");
                condition.Property(c => c.IsConditionCritical).HasColumnName("IsConditionCritical");
                condition.Property(c => c.LastSyncDate).HasColumnName("LastSyncDate");
            });

        // Asset properties
        builder.Entity<Asset>().Property(a => a.Name).HasColumnName("Name").IsRequired();
        builder.Entity<Asset>().Property(a => a.AssetType).HasColumnName("AssetType").IsRequired();
        builder.Entity<Asset>().Property(a => a.Location).HasColumnName("Location").IsRequired();
        builder.Entity<Asset>().Property(a => a.ResponsibleUserId).HasColumnName("ResponsibleUserId").IsRequired();
        builder.Entity<Asset>().Property(a => a.Status).HasColumnName("Status").IsRequired();

        // Audit columns
        builder.Entity<Asset>().Property(a => a.CreatedDate).HasColumnName("CreatedAt");
        builder.Entity<Asset>().Property(a => a.UpdatedDate).HasColumnName("UpdatedAt");
    }
}
