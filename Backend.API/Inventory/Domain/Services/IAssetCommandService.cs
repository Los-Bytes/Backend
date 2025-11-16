using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Inventory.Domain.Model.Commands;

namespace Backend.API.Inventory.Domain.Services;

/// <summary>
///     Asset command service interface
/// </summary>
public interface IAssetCommandService
{
    /// <summary>
    ///     Handle create asset command
    /// </summary>
    Task<Asset?> Handle(CreateAssetCommand command);

    /// <summary>
    ///     Handle update asset location command
    /// </summary>
    Task<Asset?> Handle(UpdateAssetLocationCommand command);

    /// <summary>
    ///     Handle update asset condition command
    /// </summary>
    Task<Asset?> Handle(UpdateAssetConditionCommand command);
}