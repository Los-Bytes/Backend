using Microsoft.EntityFrameworkCore;
using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context) 
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> FindActiveByUserIdAsync(int userId)
    {
        return await Context.Set<Subscription>()
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive);
    }

    public async Task<IEnumerable<Subscription>> FindAllByUserIdAsync(int userId)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
}