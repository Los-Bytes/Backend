namespace Backend.API.Inventory.Interfaces.REST.Resources;

/// <summary>
///     Asset resource for REST API
/// </summary>
/// <param name="Id">The unique identifier of the asset</param>
/// <param name="Name">The name of the asset</param>
/// <param name="RfidTagId">The RFID tag identifier</param>
/// <param name="AssetType">The type of asset</param>
/// <param name="Location">The current location of the asset</param>
/// <param name="ResponsibleUserId">The user profile id responsible for this asset</param>
/// <param name="Status">The status of the asset (Active, Inactive, InMaintenance, Critical)</param>
/// <param name="Temperature">Current temperature in Celsius</param>
/// <param name="Humidity">Current humidity percentage</param>
/// <param name="IsConditionCritical">Whether condition is critical</param>
/// <param name="LastSyncDate">Last synchronization date from IoT sensor</param>
public record AssetResource(
    int Id,
    string Name,
    string RfidTagId,
    string AssetType,
    string Location,
    int ResponsibleUserId,
    string Status,
    double Temperature,
    double Humidity,
    bool IsConditionCritical,
    DateTimeOffset? LastSyncDate);