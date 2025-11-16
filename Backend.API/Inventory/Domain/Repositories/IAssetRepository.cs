using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Inventory.Domain.Repositories;

/// <summary>
///     Asset repository interface
/// </summary>
public interface IAssetRepository : IBaseRepository<Asset>
{
    /// <summary>
    ///     Find assets by responsible user
    /// </summary>
    /// <param name="responsibleUserId">The user profile id</param>
    /// <returns>List of assets assigned to the user</returns>
    Task<IEnumerable<Asset>> FindAssetsByResponsibleUserAsync(int responsibleUserId);

    /// <summary>
    ///     Find asset by RFID tag id
    /// </summary>
    /// <param name="rfidTagId">The RFID tag identifier</param>
    /// <returns>The asset if found, otherwise null</returns>
    Task<Asset?> FindAssetByRfidTagAsync(string rfidTagId);
}