using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Domain.Model.Queries;
using Backend.API.Laboratories.Domain.Services;

namespace Backend.API.Laboratories.Application.ACL;

/// <summary>
///     Facade for the laboratory context
///     Provides a simplified interface for other bounded contexts
/// </summary>
public class LaboratoryContextFacade(
    ILaboratoryCommandService laboratoryCommandService,
    ILaboratoryQueryService laboratoryQueryService)
{
    /// <summary>
    ///     Creates a new laboratory and returns its identifier
    /// </summary>
    public async Task<int> CreateLaboratory(string name, string address, string phone, int capacity, 
        int adminUserId, int? labResponsibleId = null)
    {
        var command = new CreateLaboratoryCommand(
            name, address, phone, capacity, DateTime.UtcNow,
            labResponsibleId, adminUserId,
            labResponsibleId.HasValue ? new List<int> { labResponsibleId.Value } : null
        );
        
        var laboratory = await laboratoryCommandService.Handle(command);
        return laboratory?.Id ?? 0;
    }

    /// <summary>
    ///     Fetches all laboratories accessible by a user
    /// </summary>
    public async Task<IEnumerable<int>> FetchUserLaboratoryIds(int userId)
    {
        var query = new GetLaboratoriesByMemberIdQuery(userId);
        var laboratories = await laboratoryQueryService.Handle(query);
        return laboratories.Select(lab => lab.Id);
    }

    /// <summary>
    ///     Checks if a user has access to a laboratory
    /// </summary>
    public async Task<bool> HasLabAccess(int userId, int labId)
    {
        var query = new GetLaboratoryByIdQuery(labId);
        var laboratory = await laboratoryQueryService.Handle(query);
        return laboratory != null && laboratory.HasAccess(userId);
    }

    /// <summary>
    ///     Checks if a user is admin of a laboratory
    /// </summary>
    public async Task<bool> IsLabAdmin(int userId, int labId)
    {
        var query = new GetLaboratoryByIdQuery(labId);
        var laboratory = await laboratoryQueryService.Handle(query);
        return laboratory != null && laboratory.IsAdmin(userId);
    }
}
