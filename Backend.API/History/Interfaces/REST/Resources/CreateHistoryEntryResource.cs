namespace Backend.API.History.Interfaces.REST.Resources;

/// <summary>
/// Resource to create a new history entry.
/// </summary>
public record CreateHistoryEntryResource(
    int? InventoryItemId,
    int? LaboratoryId,
    string Action,
    string PreviousStatus,
    string NewStatus,
    int Quantity,
    int? UserId,
    string UserName,
    DateTime? Timestamp,
    string Description);