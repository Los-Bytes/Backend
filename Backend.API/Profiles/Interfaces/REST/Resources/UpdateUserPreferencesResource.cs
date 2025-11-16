namespace Backend.API.Profiles.Interfaces.REST.Resources;

/// <summary>
///     Resource for updating user notification preferences
/// </summary>
/// <param name="NotificationsEnabled">
///     Whether notifications are enabled
/// </param>
/// <param name="EmailNotifications">
///     Whether email notifications are enabled
/// </param>
/// <param name="PushNotifications">
///     Whether push notifications are enabled
/// </param>
public record UpdateUserPreferencesResource(
    bool NotificationsEnabled,
    bool EmailNotifications,
    bool PushNotifications);