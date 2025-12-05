namespace Backend.API.History.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new history entry.
    /// </summary>
    public record CreateHistoryEntryCommand(
        int? InventoryItemId,
        int? LaboratoryId,
        string Action,
        string PreviousStatus,
        string NewStatus,
        int Quantity,
        int? UserId,
        string UserName,
        DateTime Timestamp,
        string Description);
}