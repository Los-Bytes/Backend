using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;

namespace Backend.API.Subscriptions.Domain.Services;

/// <summary>
///     Subscription plan query service interface
/// </summary>
public interface ISubscriptionPlanQueryService
{
    /// <summary>
    ///     Handle get all plans query
    /// </summary>
    /// <param name="query">The <see cref="GetAllPlansQuery" /> query</param>
    /// <returns>A collection of <see cref="SubscriptionPlan" /> objects</returns>
    Task<IEnumerable<SubscriptionPlan>> Handle(GetAllPlansQuery query);

    /// <summary>
    ///     Handle get plan by name query
    /// </summary>
    /// <param name="query">The <see cref="GetPlanByNameQuery" /> query</param>
    /// <returns>A <see cref="SubscriptionPlan" /> object or null</returns>
    Task<SubscriptionPlan?> Handle(GetPlanByNameQuery query);
}