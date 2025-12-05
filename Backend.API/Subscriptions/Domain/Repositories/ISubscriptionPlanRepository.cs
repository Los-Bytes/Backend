using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Subscriptions.Domain.Repositories;

public interface ISubscriptionPlanRepository : IBaseRepository<SubscriptionPlan>
{
    Task<SubscriptionPlan?> FindByNameAsync(string name);
}