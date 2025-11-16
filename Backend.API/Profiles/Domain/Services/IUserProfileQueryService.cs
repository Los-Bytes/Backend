using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Domain.Model.Queries;

namespace Backend.API.Profiles.Domain.Services;

/// <summary>
///     User profile query service interface
/// </summary>
public interface IUserProfileQueryService
{
    /// <summary>
    ///     Handle get all user profiles query
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetAllUserProfilesQuery" /> query
    /// </param>
    /// <returns>
    ///     A list of <see cref="UserProfile" /> objects
    /// </returns>
    Task<IEnumerable<UserProfile>> Handle(GetAllUserProfilesQuery query);

    /// <summary>
    ///     Handle get user profile by email query
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetUserProfileByEmailQuery" /> query
    /// </param>
    /// <returns>
    ///     A <see cref="UserProfile" /> object or null
    /// </returns>
    Task<UserProfile?> Handle(GetUserProfileByEmailQuery query);

    /// <summary>
    ///     Handle get user profile by id query
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetUserProfileByIdQuery" /> query
    /// </param>
    /// <returns>
    ///     A <see cref="UserProfile" /> object or null
    /// </returns>
    Task<UserProfile?> Handle(GetUserProfileByIdQuery query);
}