namespace Backend.API.Inventory.Domain.Model.Queries;

/// <summary>
///     Get Asset by Id Query
/// </summary>
/// <param name="AssetId">The unique identifier of the asset</param>
public record GetAssetByIdQuery(int AssetId);