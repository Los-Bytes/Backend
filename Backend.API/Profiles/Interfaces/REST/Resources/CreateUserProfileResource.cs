namespace Backend.API.Profiles.Interfaces.REST.Resources;

/// <summary>
///     Resource for creating a new user profile
/// </summary>
/// <param name="FirstName">
///     The first name of the user profile
/// </param>
/// <param name="LastName">
///     The last name of the user profile
/// </param>
/// <param name="Email">
///     The email of the user profile
/// </param>
public record CreateUserProfileResource(
    string FirstName,
    string LastName,
    string Email);