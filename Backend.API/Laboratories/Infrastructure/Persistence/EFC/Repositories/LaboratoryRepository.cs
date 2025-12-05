using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Laboratories.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Laboratory repository implementation
/// </summary>
public class LaboratoryRepository(AppDbContext context)
    : BaseRepository<Laboratory>(context), ILaboratoryRepository
{
    public async Task<IEnumerable<Laboratory>> FindByAdminUserIdAsync(int adminUserId)
    {
        return await Context.Set<Laboratory>()
            .Where(l => l.AdminUserId == adminUserId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Laboratory>> FindByMemberUserIdAsync(int userId)
    {
        var labs = await Context.Set<Laboratory>().ToListAsync();
        return labs.Where(l => l.Members.Contains(userId));
    }

    public async Task<IEnumerable<Laboratory>> FindAllByUserIdAsync(int userId)
    {
        var labs = await Context.Set<Laboratory>().ToListAsync();
        return labs.Where(l => l.HasAccess(userId));
    }
}