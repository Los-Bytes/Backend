using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Subscription plan repository implementation
/// </summary>
/// <remarks>
///     Provides data access operations for subscription plans using Entity Framework Core.
/// </remarks>
/// <param name="context">
///     The database context
/// </param>
public class SubscriptionPlanRepository(AppDbContext context)
    : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{
    /// <inheritdoc />
    public async Task<SubscriptionPlan?> FindByNameAsync(string name)
    {
        return await Context.Set<SubscriptionPlan>()
            .FirstOrDefaultAsync(p => p.Name == name);
    }
}