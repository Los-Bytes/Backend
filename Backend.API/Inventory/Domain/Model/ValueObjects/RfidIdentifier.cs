namespace Backend.API.Inventory.Domain.Model.ValueObjects;

/// <summary>
///     RFID identifier value object
/// </summary>
/// <param name="TagId">The RFID tag identifier</param>
public record RfidIdentifier(string TagId)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RfidIdentifier" /> record with an empty tag id.
    /// </summary>
    public RfidIdentifier() : this(string.Empty)
    {
    }
}