using Backend.API.History.Domain.Model.Commands;
using Backend.API.History.Interfaces.REST.Resources;

namespace Backend.API.History.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert resources into history entry commands.
/// </summary>
public static class CreateHistoryEntryCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateHistoryEntryResource"/> into a <see cref="CreateHistoryEntryCommand"/>.
    /// </summary>
    public static CreateHistoryEntryCommand ToCommand(CreateHistoryEntryResource resource)
    {
        var timestamp = resource.Timestamp ?? DateTime.UtcNow;

        return new CreateHistoryEntryCommand(
            resource.InventoryItemId,
            resource.LaboratoryId,
            resource.Action,
            resource.PreviousStatus,
            resource.NewStatus,
            resource.Quantity,
            resource.UserId,
            resource.UserName,
            timestamp,
            resource.Description);
    }
}