namespace Backend.API.Laboratories.Domain.Model.Commands;

/// <summary>
///     Update Laboratory Command
/// </summary>
public record UpdateLaboratoryCommand(
    int Id,
    string Name,
    string Address,
    string Phone,
    int Capacity);