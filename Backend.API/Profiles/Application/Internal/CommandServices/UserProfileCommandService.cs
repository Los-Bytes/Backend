using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Domain.Model.Commands;
using Backend.API.Profiles.Domain.Model.Queries;
using Backend.API.Profiles.Domain.Repositories;
using Backend.API.Profiles.Domain.Services;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Profiles.Application.Internal.CommandServices;

/// <summary>
///     User profile command service
/// </summary>
/// <param name="userProfileRepository">
///     User profile repository
/// </param>
/// <param name="userProfileQueryService">
///     User profile query service
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class UserProfileCommandService(
    IUserProfileRepository userProfileRepository,
    IUserProfileQueryService userProfileQueryService,
    IUnitOfWork unitOfWork
) : IUserProfileCommandService
{
    /// <inheritdoc />
    public async Task<UserProfile?> Handle(CreateUserProfileCommand command)
    {
        var userProfile = new UserProfile(command);
        try
        {
            await userProfileRepository.AddAsync(userProfile);
            await unitOfWork.CompleteAsync();
            return userProfile;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<UserProfile?> Handle(UpdateUserPreferencesCommand command)
    {
        try
        {
            var getUserProfileByIdQuery = new GetUserProfileByIdQuery(command.UserId);
            var userProfile = await userProfileQueryService.Handle(getUserProfileByIdQuery);

            if (userProfile == null)
                return null;

            var updatedPreferences = new Backend.API.Profiles.Domain.Model.ValueObjects.UserPreferences(
                command.NotificationsEnabled,
                command.EmailNotifications,
                command.PushNotifications
            );

            userProfile.UpdatePreferences(updatedPreferences);
            userProfileRepository.Update(userProfile);
            await unitOfWork.CompleteAsync();
            return userProfile;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }
}
