using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Domain.Model.Commands;

namespace Backend.API.Profiles.Domain.Services;

/// <summary>
///     User profile command service interface
/// </summary>
public interface IUserProfileCommandService
{
    /// <summary>
    ///     Handle create user profile command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateUserProfileCommand" /> command
    /// </param>
    /// <returns>
    ///     The <see cref="UserProfile" /> object with the created profile
    /// </returns>
    Task<UserProfile?> Handle(CreateUserProfileCommand command);

    /// <summary>
    ///     Handle update user preferences command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateUserPreferencesCommand" /> command
    /// </param>
    /// <returns>
    ///     The <see cref="UserProfile" /> object with the updated preferences
    /// </returns>
    Task<UserProfile?> Handle(UpdateUserPreferencesCommand command);
}