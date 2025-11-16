using Backend.API.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model Builder Extensions for Profiles Context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the user profiles configuration
    /// </summary>
    /// <param name="builder">The model builder</param>
    public static void ApplyUserProfilesConfiguration(this ModelBuilder builder)
    {
        // User Profiles Context

        builder.Entity<UserProfile>().HasKey(p => p.Id);
        builder.Entity<UserProfile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

        // PersonName Value Object
        builder.Entity<UserProfile>().OwnsOne(p => p.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.FirstName).HasColumnName("FirstName");
                n.Property(p => p.LastName).HasColumnName("LastName");
            });

        // EmailAddress Value Object
        builder.Entity<UserProfile>().OwnsOne(p => p.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(a => a.Address).HasColumnName("EmailAddress");
            });

        // UserPreferences Value Object
        builder.Entity<UserProfile>().OwnsOne(p => p.Preferences,
            pref =>
            {
                pref.WithOwner().HasForeignKey("Id");
                pref.Property(p => p.NotificationsEnabled).HasColumnName("NotificationsEnabled");
                pref.Property(p => p.EmailNotifications).HasColumnName("EmailNotifications");
                pref.Property(p => p.PushNotifications).HasColumnName("PushNotifications");
            });

        // Audit columns
        builder.Entity<UserProfile>().Property(p => p.CreatedDate).HasColumnName("CreatedAt");
        builder.Entity<UserProfile>().Property(p => p.UpdatedDate).HasColumnName("UpdatedAt");
    }
}
