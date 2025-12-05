namespace Backend.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Get Subscription by Id Query
/// </summary>
/// <param name="Id">
///     The unique identifier of the subscription to retrieve
/// </param>
public record GetSubscriptionByIdQuery(int Id);