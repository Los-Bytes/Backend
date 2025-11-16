namespace Backend.API.Inventory.Interfaces.REST.Resources;

/// <summary>
///     Resource for updating asset condition from IoT sensors
/// </summary>
/// <param name="Temperature">Current temperature in Celsius</param>
/// <param name="Humidity">Current humidity percentage</param>
/// <param name="IsConditionCritical">Whether condition is critical</param>
public record UpdateAssetConditionResource(
    double Temperature,
    double Humidity,
    bool IsConditionCritical);