using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Inventory.Domain.Model.Queries;

namespace Backend.API.Inventory.Domain.Services;

/// <summary>
///     Asset query service interface
/// </summary>
public interface IAssetQueryService
{
    /// <summary>
    ///     Handle get all assets query
    /// </summary>
    Task<IEnumerable<Asset>> Handle(GetAllAssetsQuery query);

    /// <summary>
    ///     Handle get asset by id query
    /// </summary>
    Task<Asset?> Handle(GetAssetByIdQuery query);

    /// <summary>
    ///     Handle get assets by responsible user query
    /// </summary>
    Task<IEnumerable<Asset>> Handle(GetAssetsByResponsibleUserQuery query);
    
    /// <summary>
    ///     Handle get assets by rfid query
    /// </summary>
    Task<Asset?> Handle(GetAssetByRfidTagQuery query);
}