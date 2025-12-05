using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Subscription repository implementation
/// </summary>
/// <remarks>
///     Provides data access operations for subscriptions using Entity Framework Core.
/// </remarks>
/// <param name="context">
///     The database context
/// </param>
public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    /// <inheritdoc />
    public async Task<Subscription?> FindActiveByUserIdAsync(int userId)
    {
        return await Context.Set<Subscription>()
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Subscription>> FindAllByUserIdAsync(int userId)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
}