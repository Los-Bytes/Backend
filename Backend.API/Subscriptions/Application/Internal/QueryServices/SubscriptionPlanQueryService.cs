using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;

namespace Backend.API.Subscriptions.Application.Internal.QueryServices;

public class SubscriptionPlanQueryService(ISubscriptionPlanRepository planRepository) 
    : ISubscriptionPlanQueryService
{
    public async Task<IEnumerable<SubscriptionPlan>> Handle(GetAllPlansQuery query)
    {
        return await planRepository.ListAsync();
    }

    public async Task<SubscriptionPlan?> Handle(GetPlanByNameQuery query)
    {
        return await planRepository.FindByNameAsync(query.Name);
    }
}