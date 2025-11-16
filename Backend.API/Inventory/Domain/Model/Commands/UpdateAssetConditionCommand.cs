namespace Backend.API.Inventory.Domain.Model.Commands;

/// <summary>
///     Update Asset Condition Command (for IoT sensor data)
/// </summary>
/// <param name="AssetId">The unique identifier of the asset.</param>
/// <param name="Temperature">Current temperature in Celsius.</param>
/// <param name="Humidity">Current humidity percentage.</param>
/// <param name="IsConditionCritical">Whether condition is critical.</param>
public record UpdateAssetConditionCommand(
    int AssetId,
    double Temperature,
    double Humidity,
    bool IsConditionCritical);