using Backend.API.Inventory.Domain.Model.Commands;
using Backend.API.Inventory.Interfaces.REST.Resources;

namespace Backend.API.Inventory.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an UpdateAssetConditionCommand command from a resource
/// </summary>
public static class UpdateAssetConditionCommandFromResourceAssembler
{
    /// <summary>
    ///     Create an UpdateAssetConditionCommand command from a resource
    /// </summary>
    /// <param name="assetId">The asset identifier</param>
    /// <param name="resource">
    ///     The <see cref="UpdateAssetConditionResource" /> resource to create the command from
    /// </param>
    /// <returns>
    ///     The <see cref="UpdateAssetConditionCommand" /> command created from the resource
    /// </returns>
    public static UpdateAssetConditionCommand ToCommandFromResource(int assetId, 
        UpdateAssetConditionResource resource)
    {
        return new UpdateAssetConditionCommand(
            assetId,
            resource.Temperature,
            resource.Humidity,
            resource.IsConditionCritical
        );
    }
}