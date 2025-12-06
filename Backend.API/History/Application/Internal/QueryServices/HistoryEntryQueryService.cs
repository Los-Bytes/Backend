using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.History.Domain.Model.Queries;
using Backend.API.History.Domain.Repositories;
using Backend.API.History.Domain.Services;

namespace Backend.API.History.Application.Internal.QueryServices
{
    /// <summary>
    /// Query service implementation for history entries.
    /// </summary>
    public class HistoryEntryQueryService : IHistoryEntryQueryService
    {
        private readonly IHistoryEntryRepository _historyEntryRepository;

        public HistoryEntryQueryService(IHistoryEntryRepository historyEntryRepository)
        {
            _historyEntryRepository = historyEntryRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<HistoryEntry>> Handle(GetAllHistoryEntriesQuery query)
        {
            return await _historyEntryRepository.ListOrderedAsync();
        }

        /// <inheritdoc />
        public async Task<HistoryEntry?> Handle(GetHistoryEntryByIdQuery query)
        {
            return await _historyEntryRepository.FindByIdAsync(query.HistoryEntryId);
        }
    }
}