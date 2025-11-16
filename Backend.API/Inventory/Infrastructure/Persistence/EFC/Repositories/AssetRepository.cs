using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Inventory.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Asset repository implementation
/// </summary>
/// <remarks>
///     Provides data access operations for assets using Entity Framework Core.
/// </remarks>
/// <param name="context">
///     The database context
/// </param>
public class AssetRepository(AppDbContext context)
    : BaseRepository<Asset>(context), IAssetRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Asset>> FindAssetsByResponsibleUserAsync(int responsibleUserId)
    {
        return await Context.Set<Asset>()
            .Where(a => a.ResponsibleUserId == responsibleUserId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Asset?> FindAssetByRfidTagAsync(string rfidTagId)
    {
        return await Context.Set<Asset>()
            .FirstOrDefaultAsync(a => a.RfidTag.TagId == rfidTagId);
    }
}