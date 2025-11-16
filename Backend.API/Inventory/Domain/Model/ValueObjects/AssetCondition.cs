namespace Backend.API.Inventory.Domain.Model.ValueObjects;

/// <summary>
///     Value object for asset condition monitoring data
/// </summary>
/// <param name="Temperature">Current temperature in Celsius</param>
/// <param name="Humidity">Current humidity percentage</param>
/// <param name="IsConditionCritical">Whether condition is critical</param>
/// <param name="LastSyncDate">Last synchronization date from IoT sensor</param>
public record AssetCondition(
    double Temperature = 0.0,
    double Humidity = 0.0,
    bool IsConditionCritical = false,
    DateTimeOffset? LastSyncDate = null)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AssetCondition" /> record with default values.
    /// </summary>
    public AssetCondition() : this(0.0, 0.0, false, null)
    {
    }
}