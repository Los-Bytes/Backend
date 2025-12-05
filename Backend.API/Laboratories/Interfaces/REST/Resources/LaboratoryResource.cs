namespace Backend.API.Laboratories.Interfaces.REST.Resources;

/// <summary>
///     Laboratory Resource
/// </summary>
public record LaboratoryResource(
    int Id,
    string Name,
    string Address,
    string Phone,
    int Capacity,
    DateTime RegistrationDate,
    int? LabResponsibleId,
    int AdminUserId,
    List<int> MemberUserIds);