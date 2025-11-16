namespace Backend.API.Inventory.Domain.Model.ValueObjects;

/// <summary>
///     Asset status enumeration
/// </summary>
public enum AssetStatus
{
    /// <summary>
    ///     Asset is active and in use
    /// </summary>
    Active = 1,

    /// <summary>
    ///     Asset is inactive or decommissioned
    /// </summary>
    Inactive = 2,

    /// <summary>
    ///     Asset is in maintenance
    /// </summary>
    InMaintenance = 3,

    /// <summary>
    ///     Asset condition is critical
    /// </summary>
    Critical = 4
}