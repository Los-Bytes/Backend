using Backend.API.Profiles.Domain.Model.Aggregates;
using Backend.API.Profiles.Domain.Model.ValueObjects;
using Backend.API.Profiles.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     User profile repository implementation
/// </summary>
/// <remarks>
///     Provides data access operations for user profiles using Entity Framework Core.
/// </remarks>
/// <param name="context">
///     The database context
/// </param>
public class UserProfileRepository(AppDbContext context)
    : BaseRepository<UserProfile>(context), IUserProfileRepository
{
    /// <inheritdoc />
    public async Task<UserProfile?> FindUserProfileByEmailAsync(EmailAddress email)
    {
        return await Context.Set<UserProfile>()
            .FirstOrDefaultAsync(p => p.Email == email);
    }
}