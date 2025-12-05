using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.History.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.History.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Represents the history entry repository in the Backend API.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class HistoryEntryRepository(AppDbContext context)
    : BaseRepository<HistoryEntry>(context), IHistoryEntryRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<HistoryEntry>> ListOrderedAsync()
    {
        return await Context.Set<HistoryEntry>()
            .OrderByDescending(entry => entry.Timestamp)
            .ToListAsync();
    }
}