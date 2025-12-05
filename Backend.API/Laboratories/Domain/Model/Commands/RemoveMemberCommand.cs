namespace Backend.API.Laboratories.Domain.Model.Commands;

/// <summary>
///     Remove Member from Laboratory Command
/// </summary>
public record RemoveMemberCommand(
    int LaboratoryId,
    int UserId);