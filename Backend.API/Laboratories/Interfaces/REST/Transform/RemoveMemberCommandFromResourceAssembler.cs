using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Interfaces.REST.Resources;

namespace Backend.API.Laboratories.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a RemoveMemberCommand from a resource
/// </summary>
public static class RemoveMemberCommandFromResourceAssembler
{
    public static RemoveMemberCommand ToCommandFromResource(int labId, RemoveMemberResource resource)
    {
        return new RemoveMemberCommand(labId, resource.UserId);
    }
}