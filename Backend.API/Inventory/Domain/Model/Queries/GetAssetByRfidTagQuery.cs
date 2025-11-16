namespace Backend.API.Inventory.Domain.Model.Queries;

/// <summary>
///     Get Asset by RFID Tag Query
/// </summary>
/// <param name="RfidTagId">The RFID tag identifier</param>
public record GetAssetByRfidTagQuery(string RfidTagId);