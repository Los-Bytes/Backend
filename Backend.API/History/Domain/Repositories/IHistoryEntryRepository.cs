using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.History.Domain.Repositories
{
    /// <summary>
    /// History entry repository contract.
    /// </summary>
    public interface IHistoryEntryRepository : IBaseRepository<HistoryEntry>
    {
        /// <summary>
        /// Lists all history entries ordered by timestamp descending.
        /// </summary>
        Task<IEnumerable<HistoryEntry>> ListOrderedAsync();
    }
}