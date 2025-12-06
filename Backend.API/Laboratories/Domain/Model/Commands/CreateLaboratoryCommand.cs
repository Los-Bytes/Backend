namespace Backend.API.Laboratories.Domain.Model.Commands;

/// <summary>
///     Create Laboratory Command
/// </summary>
public record CreateLaboratoryCommand(
    string Name,
    string Address,
    string Phone,
    int Capacity,
    DateTime RegistrationDate,
    int? LabResponsibleId,
    int AdminUserId,
    List<int>? MemberUserIds);