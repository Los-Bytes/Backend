using Backend.API.Inventory.Domain.Model.Commands;
using Backend.API.Inventory.Domain.Model.ValueObjects;

namespace Backend.API.Inventory.Domain.Model.Aggregates;

/// <summary>
///     Asset Aggregate Root
/// </summary>
/// <remarks>
///     Represents a physical or digital asset in the laboratory.
///     Tracks its identification, location, condition monitoring, and responsible user.
/// </remarks>
public partial class Asset
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Asset" /> class with default values.
    /// </summary>
    public Asset()
    {
        RfidTag = new RfidIdentifier();
        AssetCondition = new AssetCondition();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Asset" /> class with the specified details.
    /// </summary>
    /// <param name="name">The name of the asset.</param>
    /// <param name="rfidTagId">The RFID tag identifier.</param>
    /// <param name="assetType">The type of asset.</param>
    /// <param name="location">The location of the asset.</param>
    /// <param name="responsibleUserId">The user profile id responsible for this asset.</param>
    public Asset(string name, string rfidTagId, string assetType, string location, int responsibleUserId)
    {
        Name = name;
        RfidTag = new RfidIdentifier(rfidTagId);
        AssetType = assetType;
        Location = location;
        ResponsibleUserId = responsibleUserId;
        Status = AssetStatus.Active;
        AssetCondition = new AssetCondition();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Asset" /> class from a create asset command.
    /// </summary>
    /// <param name="command">The create asset command containing the asset details.</param>
    public Asset(CreateAssetCommand command)
    {
        Name = command.Name;
        RfidTag = new RfidIdentifier(command.RfidTagId);
        AssetType = command.AssetType;
        Location = command.Location;
        ResponsibleUserId = command.ResponsibleUserId;
        Status = AssetStatus.Active;
        AssetCondition = new AssetCondition();
    }

    /// <summary>
    ///     Gets the unique identifier of the asset.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Gets the name of the asset.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Gets the RFID tag identifier of the asset.
    /// </summary>
    public RfidIdentifier RfidTag { get; private set; }

    /// <summary>
    ///     Gets the type of asset.
    /// </summary>
    public string AssetType { get; private set; }

    /// <summary>
    ///     Gets the current location of the asset.
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    ///     Gets the user profile id responsible for this asset.
    /// </summary>
    public int ResponsibleUserId { get; private set; }

    /// <summary>
    ///     Gets the status of the asset.
    /// </summary>
    public AssetStatus Status { get; private set; }

    /// <summary>
    ///     Gets the condition monitoring data of the asset.
    /// </summary>
    public AssetCondition AssetCondition { get; private set; }

    /// <summary>
    ///     Updates the location of the asset.
    /// </summary>
    /// <param name="newLocation">The new location.</param>
    public void UpdateLocation(string newLocation)
    {
        Location = newLocation;
    }

    /// <summary>
    ///     Updates the responsible user for this asset.
    /// </summary>
    /// <param name="newResponsibleUserId">The new responsible user id.</param>
    public void UpdateResponsibleUser(int newResponsibleUserId)
    {
        ResponsibleUserId = newResponsibleUserId;
    }

    /// <summary>
    ///     Updates asset condition monitoring data.
    /// </summary>
    /// <param name="newCondition">The new condition data.</param>
    public void UpdateCondition(AssetCondition newCondition)
    {
        AssetCondition = newCondition;
    }

    /// <summary>
    ///     Marks the asset as inactive.
    /// </summary>
    public void Deactivate()
    {
        Status = AssetStatus.Inactive;
    }
}
