namespace Backend.API.History.Domain.Model.Queries
{
    /// <summary>
    /// Query to retrieve a history entry by its identifier.
    /// </summary>
    /// <param name="HistoryEntryId">The history entry identifier.</param>
    public record GetHistoryEntryByIdQuery(int HistoryEntryId);
}