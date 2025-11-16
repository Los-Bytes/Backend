using Backend.API.Profiles.Domain.Model.Commands;
using Backend.API.Profiles.Domain.Model.Queries;
using Backend.API.Profiles.Domain.Model.ValueObjects;
using Backend.API.Profiles.Domain.Services;

namespace Backend.API.Profiles.Application.ACL;

/// <summary>
///     Facade for the user profiles context
/// </summary>
/// <param name="userProfileCommandService">
///     The user profile command service
/// </param>
/// <param name="userProfileQueryService">
///     The user profile query service
/// </param>
public class UserProfilesContextFacade(
    IUserProfileCommandService userProfileCommandService,
    IUserProfileQueryService userProfileQueryService)
{
    /// <summary>
    ///     Creates a new user profile and returns its identifier
    /// </summary>
    public async Task<int> CreateUserProfile(string firstName, string lastName, string email)
    {
        var createUserProfileCommand = new CreateUserProfileCommand(firstName, lastName, email);
        var userProfile = await userProfileCommandService.Handle(createUserProfileCommand);
        return userProfile?.Id ?? 0;
    }

    /// <summary>
    ///     Fetches the user profile identifier by email
    /// </summary>
    public async Task<int> FetchUserProfileIdByEmail(string email)
    {
        var getUserProfileByEmailQuery = new GetUserProfileByEmailQuery(new EmailAddress(email));
        var userProfile = await userProfileQueryService.Handle(getUserProfileByEmailQuery);
        return userProfile?.Id ?? 0;
    }

    /// <summary>
    ///     Updates user notification preferences
    /// </summary>
    public async Task<bool> UpdateUserPreferences(int userId, bool notificationsEnabled, 
        bool emailNotifications, bool pushNotifications)
    {
        var updateUserPreferencesCommand = new UpdateUserPreferencesCommand(userId, 
            notificationsEnabled, emailNotifications, pushNotifications);
        var updatedUserProfile = await userProfileCommandService.Handle(updateUserPreferencesCommand);
        return updatedUserProfile != null;
    }
}
