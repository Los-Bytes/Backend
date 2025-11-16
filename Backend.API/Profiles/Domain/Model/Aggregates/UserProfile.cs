using Backend.API.Profiles.Domain.Model.Commands;
using Backend.API.Profiles.Domain.Model.ValueObjects;

namespace Backend.API.Profiles.Domain.Model.Aggregates;

/// <summary>
///     UserProfile Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the UserProfile aggregate root for LabIoT.
///     It contains the properties and methods to manage user profile information and notification preferences.
/// </remarks>
public partial class UserProfile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserProfile" /> class with default values.
    /// </summary>
    public UserProfile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Preferences = new UserPreferences();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserProfile" /> class with the specified details.
    /// </summary>
    /// <param name="firstName">The first name of the person.</param>
    /// <param name="lastName">The last name of the person.</param>
    /// <param name="email">The email address.</param>
    public UserProfile(string firstName, string lastName, string email)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Preferences = new UserPreferences();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserProfile" /> class from a create user profile command.
    /// </summary>
    /// <param name="command">The create user profile command containing the profile details.</param>
    public UserProfile(CreateUserProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Preferences = new UserPreferences();
    }

    /// <summary>
    ///     Gets the unique identifier of the user profile.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Gets the name of the user profile.
    /// </summary>
    public PersonName Name { get; }

    /// <summary>
    ///     Gets the email address of the user profile.
    /// </summary>
    public EmailAddress Email { get; }

    /// <summary>
    ///     Gets the notification preferences of the user profile.
    /// </summary>
    public UserPreferences Preferences { get; private set; }

    /// <summary>
    ///     Gets the full name of the user profile.
    /// </summary>
    public string FullName => Name.FullName;

    /// <summary>
    ///     Gets the email address string of the user profile.
    /// </summary>
    public string EmailAddress => Email.Address;

    /// <summary>
    ///     Updates user preferences.
    /// </summary>
    /// <param name="preferences">The new preferences.</param>
    public void UpdatePreferences(UserPreferences preferences)
    {
        Preferences = preferences;
    }
}
