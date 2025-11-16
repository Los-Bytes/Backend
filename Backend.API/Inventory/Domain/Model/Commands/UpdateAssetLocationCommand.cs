namespace Backend.API.Inventory.Domain.Model.Commands;

/// <summary>
///     Update Asset Location Command
/// </summary>
/// <param name="AssetId">The unique identifier of the asset.</param>
/// <param name="NewLocation">The new location of the asset.</param>
public record UpdateAssetLocationCommand(int AssetId, string NewLocation);