using Backend.API.Inventory.Domain.Model.Commands;
using Backend.API.Inventory.Domain.Model.Queries;
using Backend.API.Inventory.Domain.Model.ValueObjects;
using Backend.API.Inventory.Domain.Services;

namespace Backend.API.Inventory.Application.ACL;

/// <summary>
///     Facade for the inventory context
/// </summary>
/// <remarks>
///     This facade encapsulates the inventory context and provides
///     a clean interface for other bounded contexts to interact with assets.
/// </remarks>
/// <param name="assetCommandService">
///     The asset command service
/// </param>
/// <param name="assetQueryService">
///     The asset query service
/// </param>
public class InventoryContextFacade(
    IAssetCommandService assetCommandService,
    IAssetQueryService assetQueryService)
{
    /// <summary>
    ///     Creates a new asset and returns its identifier
    /// </summary>
    public async Task<int> CreateAsset(string name, string rfidTagId, string assetType, 
        string location, int responsibleUserId)
    {
        var createAssetCommand = new CreateAssetCommand(name, rfidTagId, assetType, location, responsibleUserId);
        var asset = await assetCommandService.Handle(createAssetCommand);
        return asset?.Id ?? 0;
    }

    /// <summary>
    ///     Fetches the asset identifier by RFID tag
    /// </summary>
    public async Task<int> FetchAssetIdByRfidTag(string rfidTagId)
    {
        var asset = await assetQueryService.Handle(new GetAssetByRfidTagQuery(rfidTagId));
        return asset?.Id ?? 0;
    }

    /// <summary>
    ///     Updates asset condition from IoT sensor data
    /// </summary>
    public async Task<bool> UpdateAssetConditionFromSensor(int assetId, double temperature, 
        double humidity, bool isCritical)
    {
        var updateAssetConditionCommand = new UpdateAssetConditionCommand(assetId, temperature, humidity, isCritical);
        var updatedAsset = await assetCommandService.Handle(updateAssetConditionCommand);
        return updatedAsset != null;
    }
}
