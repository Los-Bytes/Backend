namespace Backend.API.Inventory.Interfaces.REST.Resources;

/// <summary>
///     Resource for updating asset location
/// </summary>
/// <param name="NewLocation">The new location of the asset</param>
public record UpdateAssetLocationResource(string NewLocation);