using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;

namespace Backend.API.Subscriptions.Application.Internal.QueryServices;

/// <summary>
///     Subscription query service
/// </summary>
/// <param name="subscriptionRepository">
///     Subscription repository
/// </param>
public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) 
    : ISubscriptionQueryService
{
    /// <inheritdoc />
    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
    {
        return await subscriptionRepository.FindByIdAsync(query.Id);
    }

    /// <inheritdoc />
    public async Task<Subscription?> Handle(GetActiveSubscriptionByUserIdQuery query)
    {
        return await subscriptionRepository.FindActiveByUserIdAsync(query.UserId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByUserIdQuery query)
    {
        return await subscriptionRepository.FindAllByUserIdAsync(query.UserId);
    }
}