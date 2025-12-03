namespace Backend.API.History.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete a history entry by its identifier.
    /// </summary>
    /// <param name="HistoryEntryId">The history entry identifier.</param>
    public record DeleteHistoryEntryCommand(int HistoryEntryId);
}