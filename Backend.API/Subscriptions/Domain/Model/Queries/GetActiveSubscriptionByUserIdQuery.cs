namespace Backend.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Get Active Subscription by User Id Query
/// </summary>
/// <param name="UserId">
///     The user identifier
/// </param>
public record GetActiveSubscriptionByUserIdQuery(int UserId);