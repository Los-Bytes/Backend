using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Inventory.Interfaces.REST.Resources;

namespace Backend.API.Inventory.Interfaces.REST.Transform;

/// <summary>
///     Assembler class to convert Asset entity to AssetResource
/// </summary>
public static class AssetResourceFromEntityAssembler
{
    /// <summary>
    ///     Convert Asset entity to AssetResource
    /// </summary>
    /// <param name="entity">
    ///     <see cref="Asset" /> entity to convert
    /// </param>
    /// <returns>
    ///     <see cref="AssetResource" /> converted from <see cref="Asset" /> entity
    /// </returns>
    public static AssetResource ToResourceFromEntity(Asset entity)
    {
        return new AssetResource(
            entity.Id,
            entity.Name,
            entity.RfidTag.TagId,
            entity.AssetType,
            entity.Location,
            entity.ResponsibleUserId,
            entity.Status.ToString(),
            entity.AssetCondition.Temperature,
            entity.AssetCondition.Humidity,
            entity.AssetCondition.IsConditionCritical,
            entity.AssetCondition.LastSyncDate
        );
    }
}