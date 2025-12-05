using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Subscriptions.Domain.Repositories;

/// <summary>
///     Subscription plan repository interface
/// </summary>
public interface ISubscriptionPlanRepository : IBaseRepository<SubscriptionPlan>
{
    /// <summary>
    ///     Find a subscription plan by name
    /// </summary>
    /// <param name="name">The plan name (Free, Pro, Max)</param>
    /// <returns>The <see cref="SubscriptionPlan" /> if found, otherwise null</returns>
    Task<SubscriptionPlan?> FindByNameAsync(string name);
}