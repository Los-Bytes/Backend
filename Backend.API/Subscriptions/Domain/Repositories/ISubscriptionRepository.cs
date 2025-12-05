using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Subscriptions.Domain.Repositories;

/// <summary>
///     Subscription repository interface
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    /// <summary>
    ///     Find active subscription by user ID
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>The active <see cref="Subscription" /> if found, otherwise null</returns>
    Task<Subscription?> FindActiveByUserIdAsync(int userId);

    /// <summary>
    ///     Find all subscriptions by user ID
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>A collection of <see cref="Subscription" /> objects</returns>
    Task<IEnumerable<Subscription>> FindAllByUserIdAsync(int userId);
}