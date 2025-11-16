namespace Backend.API.Profiles.Domain.Model.Commands;

/// <summary>
///     Create User Profile Command
/// </summary>
/// <param name="FirstName">
///     The first name of the user profile.
/// </param>
/// <param name="LastName">
///     The last name of the user profile.
/// </param>
/// <param name="Email">
///     The email address of the user profile.
/// </param>
public record CreateUserProfileCommand(
    string FirstName,
    string LastName,
    string Email);