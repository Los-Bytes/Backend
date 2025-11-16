namespace Backend.API.Profiles.Domain.Model.Commands;

/// <summary>
///     Update User Preferences Command
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user profile.
/// </param>
/// <param name="NotificationsEnabled">
///     Whether notifications are enabled.
/// </param>
/// <param name="EmailNotifications">
///     Whether email notifications are enabled.
/// </param>
/// <param name="PushNotifications">
///     Whether push notifications are enabled.
/// </param>
public record UpdateUserPreferencesCommand(
    int UserId,
    bool NotificationsEnabled,
    bool EmailNotifications,
    bool PushNotifications);