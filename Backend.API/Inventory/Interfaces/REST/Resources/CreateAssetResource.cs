namespace Backend.API.Inventory.Interfaces.REST.Resources;

/// <summary>
///     Resource for creating a new asset
/// </summary>
/// <param name="Name">The name of the asset</param>
/// <param name="RfidTagId">The RFID tag identifier</param>
/// <param name="AssetType">The type of asset (equipment, material, tool, etc.)</param>
/// <param name="Location">The location of the asset</param>
/// <param name="ResponsibleUserId">The user profile id responsible for this asset</param>
public record CreateAssetResource(
    string Name,
    string RfidTagId,
    string AssetType,
    string Location,
    int ResponsibleUserId);