namespace Backend.API.Profiles.Domain.Model.Queries;

/// <summary>
///     Get User Profile by Id Query
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user profile to retrieve
/// </param>
public record GetUserProfileByIdQuery(int UserId);