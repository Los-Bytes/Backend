using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Domain.Model.Queries;
using Backend.API.Laboratories.Domain.Repositories;
using Backend.API.Laboratories.Domain.Services;

namespace Backend.API.Laboratories.Application.Internal.QueryServices;

/// <summary>
///     Laboratory query service implementation
/// </summary>
public class LaboratoryQueryService(ILaboratoryRepository laboratoryRepository) 
    : ILaboratoryQueryService
{
    public async Task<Laboratory?> Handle(GetLaboratoryByIdQuery query)
    {
        return await laboratoryRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Laboratory>> Handle(GetAllLaboratoriesQuery query)
    {
        return await laboratoryRepository.ListAsync();
    }

    public async Task<IEnumerable<Laboratory>> Handle(GetLaboratoriesByAdminIdQuery query)
    {
        return await laboratoryRepository.FindByAdminUserIdAsync(query.AdminUserId);
    }

    public async Task<IEnumerable<Laboratory>> Handle(GetLaboratoriesByMemberIdQuery query)
    {
        return await laboratoryRepository.FindAllByUserIdAsync(query.UserId);
    }
}