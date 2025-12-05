namespace Backend.API.Laboratories.Domain.Model.Commands;

/// <summary>
///     Add Member to Laboratory Command
/// </summary>
public record AddMemberCommand(
    int LaboratoryId,
    int UserId);