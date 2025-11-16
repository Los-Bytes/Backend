using Backend.API.Inventory.Domain.Model.Commands;
using Backend.API.Inventory.Interfaces.REST.Resources;

namespace Backend.API.Inventory.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an UpdateAssetLocationCommand command from a resource
/// </summary>
public static class UpdateAssetLocationCommandFromResourceAssembler
{
    /// <summary>
    ///     Create an UpdateAssetLocationCommand command from a resource
    /// </summary>
    /// <param name="assetId">The asset identifier</param>
    /// <param name="resource">
    ///     The <see cref="UpdateAssetLocationResource" /> resource to create the command from
    /// </param>
    /// <returns>
    ///     The <see cref="UpdateAssetLocationCommand" /> command created from the resource
    /// </returns>
    public static UpdateAssetLocationCommand ToCommandFromResource(int assetId, 
        UpdateAssetLocationResource resource)
    {
        return new UpdateAssetLocationCommand(assetId, resource.NewLocation);
    }
}