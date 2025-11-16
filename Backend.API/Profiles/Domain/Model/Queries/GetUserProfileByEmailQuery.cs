using Backend.API.Profiles.Domain.Model.ValueObjects;

namespace Backend.API.Profiles.Domain.Model.Queries;

/// <summary>
///     Get User Profile by Email Query
/// </summary>
/// <param name="Email">
///     The <see cref="EmailAddress" /> email address of the user profile to retrieve
/// </param>
public record GetUserProfileByEmailQuery(EmailAddress Email);