using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.History.Interfaces.REST.Resources;

namespace Backend.API.History.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert history entry entities into resources.
/// </summary>
public static class HistoryEntryResourceFromEntityAssembler
{
    public static HistoryEntryResource ToResource(HistoryEntry entity)
        => new(
            entity.Id,
            entity.InventoryItemId,
            entity.LaboratoryId,
            entity.Action,
            entity.PreviousStatus,
            entity.NewStatus,
            entity.Quantity,
            entity.UserId,
            entity.UserName,
            entity.Timestamp,
            entity.Description,
            entity.CreatedAt,
            entity.UpdatedAt);

    public static IEnumerable<HistoryEntryResource> ToResourceList(IEnumerable<HistoryEntry> entities)
        => entities.Select(ToResource);
}