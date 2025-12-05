namespace Backend.API.Laboratories.Interfaces.REST.Resources;

/// <summary>
///     Create Laboratory Resource
/// </summary>
public record CreateLaboratoryResource(
    string Name,
    string Address,
    string Phone,
    int Capacity,
    DateTime RegistrationDate,
    int? LabResponsibleId,
    int AdminUserId,
    List<int>? MemberUserIds);