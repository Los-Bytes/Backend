using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Subscription repository implementation
/// </summary>
public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> FindActiveByUserIdAsync(int userId)
    {
        var subscriptions = await Context.Set<Subscription>()
            .Where(s => s.UserId == userId)
            .ToListAsync();
        
        return subscriptions.FirstOrDefault(s => s.IsCurrentlyActive());
    }

    public async Task<IEnumerable<Subscription>> FindAllByUserIdAsync(int userId)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
}