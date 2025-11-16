using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Inventory.Domain.Model.Queries;
using Backend.API.Inventory.Domain.Repositories;
using Backend.API.Inventory.Domain.Services;

namespace Backend.API.Inventory.Application.Internal.QueryServices;

/// <summary>
///     Asset query service
/// </summary>
/// <param name="assetRepository">
///     Asset repository
/// </param>
public class AssetQueryService(IAssetRepository assetRepository) : IAssetQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Asset>> Handle(GetAllAssetsQuery query)
    {
        return await assetRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Asset?> Handle(GetAssetByIdQuery query)
    {
        return await assetRepository.FindByIdAsync(query.AssetId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Asset>> Handle(GetAssetsByResponsibleUserQuery query)
    {
        return await assetRepository.FindAssetsByResponsibleUserAsync(query.ResponsibleUserId);
    }

    /// <summary>
    ///     Handle get asset by RFID tag query
    /// </summary>
    public async Task<Asset?> Handle(GetAssetByRfidTagQuery query)
    {
        return await assetRepository.FindAssetByRfidTagAsync(query.RfidTagId);
    }
}