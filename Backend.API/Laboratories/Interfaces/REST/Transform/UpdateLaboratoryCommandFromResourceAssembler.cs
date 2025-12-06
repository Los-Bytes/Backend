using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Interfaces.REST.Resources;

namespace Backend.API.Laboratories.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an UpdateLaboratoryCommand from a resource
/// </summary>
public static class UpdateLaboratoryCommandFromResourceAssembler
{
    public static UpdateLaboratoryCommand ToCommandFromResource(int id, UpdateLaboratoryResource resource)
    {
        return new UpdateLaboratoryCommand(
            id,
            resource.Name,
            resource.Address,
            resource.Phone,
            resource.Capacity
        );
    }
}