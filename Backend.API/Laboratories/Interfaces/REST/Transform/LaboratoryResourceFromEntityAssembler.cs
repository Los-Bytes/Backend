using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Interfaces.REST.Resources;

namespace Backend.API.Laboratories.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a LaboratoryResource from a Laboratory entity
/// </summary>
public static class LaboratoryResourceFromEntityAssembler
{
    public static LaboratoryResource ToResourceFromEntity(Laboratory entity)
    {
        return new LaboratoryResource(
            entity.Id,
            entity.Name.Value,
            entity.Address.ToFullAddressString(),
            entity.Phone.Value,
            entity.Capacity.Value,
            entity.RegistrationDate,
            entity.LabResponsibleId,
            entity.AdminUserId,
            entity.Members.ToList()
        );
    }
}