using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.History.Domain.Model.Queries;

namespace Backend.API.History.Domain.Services
{
    /// <summary>
    /// Query service contract for history entries.
    /// </summary>
    public interface IHistoryEntryQueryService
    {
        /// <summary>
        /// Handles the query to retrieve all history entries.
        /// </summary>
        Task<IEnumerable<HistoryEntry>> Handle(GetAllHistoryEntriesQuery query);

        /// <summary>
        /// Handles the query to retrieve a history entry by its identifier.
        /// Returns null if not found.
        /// </summary>
        Task<HistoryEntry?> Handle(GetHistoryEntryByIdQuery query);
    }
}