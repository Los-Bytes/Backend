using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;

namespace Backend.API.Subscriptions.Application.Internal.QueryServices;

/// <summary>
///     Subscription plan query service
/// </summary>
/// <param name="subscriptionPlanRepository">
///     Subscription plan repository
/// </param>
public class SubscriptionPlanQueryService(ISubscriptionPlanRepository subscriptionPlanRepository) 
    : ISubscriptionPlanQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<SubscriptionPlan>> Handle(GetAllPlansQuery query)
    {
        return await subscriptionPlanRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<SubscriptionPlan?> Handle(GetPlanByNameQuery query)
    {
        return await subscriptionPlanRepository.FindByNameAsync(query.Name);
    }
}