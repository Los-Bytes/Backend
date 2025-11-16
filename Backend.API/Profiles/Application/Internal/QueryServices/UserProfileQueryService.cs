using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Domain.Model.Queries;
using Backend.API.Profiles.Domain.Repositories;
using Backend.API.Profiles.Domain.Services;

namespace Backend.API.Profiles.Application.Internal.QueryServices;

/// <summary>
///     User profile query service
/// </summary>
/// <param name="userProfileRepository">
///     User profile repository
/// </param>
public class UserProfileQueryService(IUserProfileRepository userProfileRepository) : IUserProfileQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<UserProfile>> Handle(GetAllUserProfilesQuery query)
    {
        return await userProfileRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<UserProfile?> Handle(GetUserProfileByEmailQuery query)
    {
        return await userProfileRepository.FindUserProfileByEmailAsync(query.Email);
    }

    /// <inheritdoc />
    public async Task<UserProfile?> Handle(GetUserProfileByIdQuery query)
    {
        return await userProfileRepository.FindByIdAsync(query.UserId);
    }
}