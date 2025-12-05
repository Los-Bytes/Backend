using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Domain.Model.Queries;

namespace Backend.API.Laboratories.Domain.Services;

/// <summary>
///     Laboratory query service interface
/// </summary>
public interface ILaboratoryQueryService
{
    Task<Laboratory?> Handle(GetLaboratoryByIdQuery query);
    Task<IEnumerable<Laboratory>> Handle(GetAllLaboratoriesQuery query);
    Task<IEnumerable<Laboratory>> Handle(GetLaboratoriesByAdminIdQuery query);
    Task<IEnumerable<Laboratory>> Handle(GetLaboratoriesByMemberIdQuery query);
}