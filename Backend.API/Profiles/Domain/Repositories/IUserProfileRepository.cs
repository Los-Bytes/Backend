using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Domain.Model.ValueObjects;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Profiles.Domain.Repositories;

/// <summary>
///     User profile repository interface
/// </summary>
public interface IUserProfileRepository : IBaseRepository<UserProfile>
{
    /// <summary>
    ///     Find a user profile by email
    /// </summary>
    /// <param name="email">
    ///     The <see cref="EmailAddress" /> email address to search for
    /// </param>
    /// <returns>
    ///     The <see cref="UserProfile" /> if found, otherwise null
    /// </returns>
    Task<UserProfile?> FindUserProfileByEmailAsync(EmailAddress email);
}