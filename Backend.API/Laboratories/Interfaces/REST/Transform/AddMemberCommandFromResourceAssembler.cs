using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Interfaces.REST.Resources;

namespace Backend.API.Laboratories.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an AddMemberCommand from a resource
/// </summary>
public static class AddMemberCommandFromResourceAssembler
{
    public static AddMemberCommand ToCommandFromResource(int labId, AddMemberResource resource)
    {
        return new AddMemberCommand(labId, resource.UserId);
    }
}