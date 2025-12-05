namespace Backend.API.History.Interfaces.REST.Resources;

/// <summary>
/// Resource that represents a history entry.
/// </summary>
public record HistoryEntryResource(
    int Id,
    int? InventoryItemId,
    int? LaboratoryId,
    string Action,
    string PreviousStatus,
    string NewStatus,
    int Quantity,
    int? UserId,
    string UserName,
    DateTime Timestamp,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt);