using Backend.API.Profiles.Domain.Model.Commands;
using Backend.API.Profiles.Interfaces.REST.Resources;

namespace Backend.API.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an UpdateUserPreferencesCommand command from a resource
/// </summary>
public static class UpdateUserPreferencesCommandFromResourceAssembler
{
    /// <summary>
    ///     Create an UpdateUserPreferencesCommand command from a resource
    /// </summary>
    /// <param name="userId">
    ///     The user profile identifier
    /// </param>
    /// <param name="resource">
    ///     The <see cref="UpdateUserPreferencesResource" /> resource to create the command from
    /// </param>
    /// <returns>
    ///     The <see cref="UpdateUserPreferencesCommand" /> command created from the resource
    /// </returns>
    public static UpdateUserPreferencesCommand ToCommandFromResource(int userId, 
        UpdateUserPreferencesResource resource)
    {
        return new UpdateUserPreferencesCommand(
            userId,
            resource.NotificationsEnabled,
            resource.EmailNotifications,
            resource.PushNotifications
        );
    }
}