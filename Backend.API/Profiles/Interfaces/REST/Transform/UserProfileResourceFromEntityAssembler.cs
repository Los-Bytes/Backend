using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Interfaces.REST.Resources;

namespace Backend.API.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Assembler class to convert UserProfile entity to UserProfileResource
/// </summary>
public static class UserProfileResourceFromEntityAssembler
{
    /// <summary>
    ///     Convert UserProfile entity to UserProfileResource
    /// </summary>
    /// <param name="entity">
    ///     <see cref="UserProfile" /> entity to convert
    /// </param>
    /// <returns>
    ///     <see cref="UserProfileResource" /> converted from <see cref="UserProfile" /> entity
    /// </returns>
    public static UserProfileResource ToResourceFromEntity(UserProfile entity)
    {
        return new UserProfileResource(
            entity.Id,
            entity.FullName,
            entity.EmailAddress,
            entity.Preferences.NotificationsEnabled,
            entity.Preferences.EmailNotifications,
            entity.Preferences.PushNotifications
        );
    }
}