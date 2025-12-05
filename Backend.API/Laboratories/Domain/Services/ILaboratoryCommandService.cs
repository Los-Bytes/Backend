using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Domain.Model.Commands;

namespace Backend.API.Laboratories.Domain.Services;

/// <summary>
///     Laboratory command service interface
/// </summary>
public interface ILaboratoryCommandService
{
    Task<Laboratory?> Handle(CreateLaboratoryCommand command);
    Task<Laboratory?> Handle(UpdateLaboratoryCommand command);
    Task<Laboratory?> Handle(AddMemberCommand command);
    Task<Laboratory?> Handle(RemoveMemberCommand command);
    Task<bool> DeleteLaboratory(int id);
}