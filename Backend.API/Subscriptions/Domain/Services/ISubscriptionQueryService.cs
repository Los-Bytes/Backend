using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;

namespace Backend.API.Subscriptions.Domain.Services;

/// <summary>
///     Subscription query service interface
/// </summary>
public interface ISubscriptionQueryService
{
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query);
    Task<Subscription?> Handle(GetActiveSubscriptionByUserIdQuery query);
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByUserIdQuery query);
}