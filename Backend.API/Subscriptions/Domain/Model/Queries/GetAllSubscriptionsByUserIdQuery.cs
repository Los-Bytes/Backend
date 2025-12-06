namespace Backend.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Get All Subscriptions by User Id Query
/// </summary>
/// <param name="UserId">
///     The user identifier
/// </param>
public record GetAllSubscriptionsByUserIdQuery(int UserId);