namespace Backend.API.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object for user notification preferences
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
public record UserPreferences(
    bool NotificationsEnabled = true,
    bool EmailNotifications = true,
    bool PushNotifications = false)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserPreferences" /> record with default values.
    /// </summary>
    public UserPreferences() : this(true, true, false)
    {
    }
}