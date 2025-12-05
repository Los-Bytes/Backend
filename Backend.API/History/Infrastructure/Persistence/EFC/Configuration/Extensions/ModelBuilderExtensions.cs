using Backend.API.History.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.History.Infrastructure.Persistence.EFC.Configuration.Extensions
{
    /// <summary>
    /// Model builder extensions for the History bounded context.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Applies History context configuration to the model builder.
        /// </summary>
        public static void ApplyHistoryConfiguration(this ModelBuilder builder)
        {
            builder.Entity<HistoryEntry>(entity =>
            {
                entity.ToTable("history_entries");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InventoryItemId)
                    .IsRequired(false);

                entity.Property(e => e.LaboratoryId)
                    .IsRequired(false);

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PreviousStatus)
                    .HasMaxLength(100);

                entity.Property(e => e.NewStatus)
                    .HasMaxLength(100);

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(200);

                entity.Property(e => e.Timestamp)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(2000);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .IsRequired();
            });
        }
    }
}