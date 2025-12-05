using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.History.Domain.Model.Commands;

namespace Backend.API.History.Domain.Services
{
    /// <summary>
    /// Command service contract for history entries.
    /// </summary>
    public interface IHistoryEntryCommandService
    {
        /// <summary>
        /// Handles the creation of a new history entry.
        /// Returns null if the operation fails.
        /// </summary>
        Task<HistoryEntry?> Handle(CreateHistoryEntryCommand command);

        /// <summary>
        /// Handles the deletion of a history entry by its identifier.
        /// Returns true if the entry was deleted, false otherwise.
        /// </summary>
        Task<bool> Handle(DeleteHistoryEntryCommand command);
    }
}