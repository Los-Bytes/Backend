using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Laboratories.Domain.Repositories;

/// <summary>
///     Laboratory repository interface
/// </summary>
public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    /// <summary>
    ///     Find laboratories by admin user ID
    /// </summary>
    Task<IEnumerable<Laboratory>> FindByAdminUserIdAsync(int adminUserId);

    /// <summary>
    ///     Find laboratories where user is a member
    /// </summary>
    Task<IEnumerable<Laboratory>> FindByMemberUserIdAsync(int userId);

    /// <summary>
    ///     Find all laboratories accessible by a user (admin or member)
    /// </summary>
    Task<IEnumerable<Laboratory>> FindAllByUserIdAsync(int userId);
}