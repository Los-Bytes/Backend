namespace Backend.API.Profiles.Interfaces.REST.Resources;

/// <summary>
///     User profile resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the user profile
/// </param>
/// <param name="FullName">
///     The full name of the user profile
/// </param>
/// <param name="Email">
///     The email address of the user profile
/// </param>
/// <param name="NotificationsEnabled">
///     Whether notifications are enabled
/// </param>
/// <param name="EmailNotifications">
///     Whether email notifications are enabled
/// </param>
/// <param name="PushNotifications">
///     Whether push notifications are enabled
/// </param>
public record UserProfileResource(
    int Id,
    string FullName,
    string Email,
    bool NotificationsEnabled,
    bool EmailNotifications,
    bool PushNotifications);