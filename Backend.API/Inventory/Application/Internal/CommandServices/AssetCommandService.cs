using Backend.API.Inventory.Domain.Model.Aggregates;
using Backend.API.Inventory.Domain.Model.Commands;
using Backend.API.Inventory.Domain.Model.Queries;
using Backend.API.Inventory.Domain.Repositories;
using Backend.API.Inventory.Domain.Services;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Inventory.Application.Internal.CommandServices;

/// <summary>
///     Asset command service
/// </summary>
/// <param name="assetRepository">
///     Asset repository
/// </param>
/// <param name="assetQueryService">
///     Asset query service
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class AssetCommandService(
    IAssetRepository assetRepository,
    IAssetQueryService assetQueryService,
    IUnitOfWork unitOfWork
) : IAssetCommandService
{
    /// <inheritdoc />
    public async Task<Asset?> Handle(CreateAssetCommand command)
    {
        var asset = new Asset(command);
        try
        {
            await assetRepository.AddAsync(asset);
            await unitOfWork.CompleteAsync();
            return asset;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Asset?> Handle(UpdateAssetLocationCommand command)
    {
        try
        {
            var getAssetByIdQuery = new GetAssetByIdQuery(command.AssetId);
            var asset = await assetQueryService.Handle(getAssetByIdQuery);

            if (asset == null)
                return null;

            asset.UpdateLocation(command.NewLocation);
            assetRepository.Update(asset);
            await unitOfWork.CompleteAsync();
            return asset;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Asset?> Handle(UpdateAssetConditionCommand command)
    {
        try
        {
            var getAssetByIdQuery = new GetAssetByIdQuery(command.AssetId);
            var asset = await assetQueryService.Handle(getAssetByIdQuery);

            if (asset == null)
                return null;

            var updatedCondition = new Backend.API.Inventory.Domain.Model.ValueObjects.AssetCondition(
                command.Temperature,
                command.Humidity,
                command.IsConditionCritical,
                DateTimeOffset.UtcNow
            );

            asset.UpdateCondition(updatedCondition);
            assetRepository.Update(asset);
            await unitOfWork.CompleteAsync();
            return asset;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }
}
