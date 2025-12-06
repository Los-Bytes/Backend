using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Interfaces.REST.Resources;

namespace Backend.API.Laboratories.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a CreateLaboratoryCommand from a resource
/// </summary>
public static class CreateLaboratoryCommandFromResourceAssembler
{
    public static CreateLaboratoryCommand ToCommandFromResource(CreateLaboratoryResource resource)
    {
        return new CreateLaboratoryCommand(
            resource.Name,
            resource.Address,
            resource.Phone,
            resource.Capacity,
            resource.RegistrationDate,
            resource.LabResponsibleId,
            resource.AdminUserId,
            resource.MemberUserIds
        );
    }
}