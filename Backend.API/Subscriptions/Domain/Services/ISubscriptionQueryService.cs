using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;

namespace Backend.API.Subscriptions.Domain.Services;

/// <summary>
///     Subscription query service interface
/// </summary>
public interface ISubscriptionQueryService
{
    /// <summary>
    ///     Handle get subscription by id query
    /// </summary>
    /// <param name="query">The <see cref="GetSubscriptionByIdQuery" /> query</param>
    /// <returns>A <see cref="Subscription" /> object or null</returns>
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query);

    /// <summary>
    ///     Handle get active subscription by user id query
    /// </summary>
    /// <param name="query">The <see cref="GetActiveSubscriptionByUserIdQuery" /> query</param>
    /// <returns>A <see cref="Subscription" /> object or null</returns>
    Task<Subscription?> Handle(GetActiveSubscriptionByUserIdQuery query);

    /// <summary>
    ///     Handle get all subscriptions by user id query
    /// </summary>
    /// <param name="query">The <see cref="GetAllSubscriptionsByUserIdQuery" /> query</param>
    /// <returns>A collection of <see cref="Subscription" /> objects</returns>
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByUserIdQuery query);
}