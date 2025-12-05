namespace Backend.API.Laboratories.Interfaces.REST.Resources;

/// <summary>
///     Update Laboratory Resource
/// </summary>
public record UpdateLaboratoryResource(
    string Name,
    string Address,
    string Phone,
    int Capacity);