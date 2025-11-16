using Backend.API.Inventory.Domain.Model.Commands;
using Backend.API.Inventory.Interfaces.REST.Resources;

namespace Backend.API.Inventory.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a CreateAssetCommand command from a resource
/// </summary>
public static class CreateAssetCommandFromResourceAssembler
{
    /// <summary>
    ///     Create a CreateAssetCommand command from a resource
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateAssetResource" /> resource to create the command from
    /// </param>
    /// <returns>
    ///     The <see cref="CreateAssetCommand" /> command created from the resource
    /// </returns>
    public static CreateAssetCommand ToCommandFromResource(CreateAssetResource resource)
    {
        return new CreateAssetCommand(
            resource.Name,
            resource.RfidTagId,
            resource.AssetType,
            resource.Location,
            resource.ResponsibleUserId
        );
    }
}