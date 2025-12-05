using Microsoft.EntityFrameworkCore;
using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionPlanRepository(AppDbContext context) 
    : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{
    public async Task<SubscriptionPlan?> FindByNameAsync(string name)
    {
        return await Context.Set<SubscriptionPlan>()
            .FirstOrDefaultAsync(p => p.Name == name);
    }
}