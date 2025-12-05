using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;

namespace Backend.API.Subscriptions.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) 
    : ISubscriptionQueryService
{
    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
    {
        return await subscriptionRepository.FindByIdAsync(query.Id);
    }

    public async Task<Subscription?> Handle(GetActiveSubscriptionByUserIdQuery query)
    {
        return await subscriptionRepository.FindActiveByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByUserIdQuery query)
    {
        return await subscriptionRepository.FindAllByUserIdAsync(query.UserId);
    }
}