using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Subscriptions.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    Task<Subscription?> FindActiveByUserIdAsync(int userId);
    Task<IEnumerable<Subscription>> FindAllByUserIdAsync(int userId);
}