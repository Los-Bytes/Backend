namespace Backend.API.Inventory.Domain.Model.Commands;

/// <summary>
///     Create Asset Command
/// </summary>
/// <param name="Name">The name of the asset.</param>
/// <param name="RfidTagId">The RFID tag identifier.</param>
/// <param name="AssetType">The type of asset.</param>
/// <param name="Location">The location of the asset.</param>
/// <param name="ResponsibleUserId">The user profile id responsible for this asset.</param>
public record CreateAssetCommand(
    string Name,
    string RfidTagId,
    string AssetType,
    string Location,
    int ResponsibleUserId);