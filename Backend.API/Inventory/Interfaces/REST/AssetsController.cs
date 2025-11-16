using System.Net.Mime;
using Backend.API.Inventory.Domain.Model.Queries;
using Backend.API.Inventory.Domain.Services;
using Backend.API.Inventory.Interfaces.REST.Resources;
using Backend.API.Inventory.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Inventory.Interfaces.REST;

/// <summary>
///     Controller for managing assets.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Inventory Endpoints.")]
public class InventoryController(
    IAssetCommandService assetCommandService,
    IAssetQueryService assetQueryService)
    : ControllerBase
{
    /// <summary>
    ///     Get an asset by its unique identifier
    /// </summary>
    [HttpGet("{assetId:int}")]
    [SwaggerOperation("Get Asset by Id", "Get an asset by its unique identifier.", OperationId = "GetAssetById")]
    [SwaggerResponse(200, "The asset was found and returned.", typeof(AssetResource))]
    [SwaggerResponse(404, "The asset was not found.")]
    public async Task<IActionResult> GetAssetById(int assetId)
    {
        var getAssetByIdQuery = new GetAssetByIdQuery(assetId);
        var asset = await assetQueryService.Handle(getAssetByIdQuery);
        if (asset is null) return NotFound();
        var assetResource = AssetResourceFromEntityAssembler.ToResourceFromEntity(asset);
        return Ok(assetResource);
    }

    /// <summary>
    ///     Get an asset by its RFID tag
    /// </summary>
    [HttpGet("by-rfid/{rfidTagId}")]
    [SwaggerOperation("Get Asset by RFID Tag", "Get an asset by its RFID tag identifier.", 
        OperationId = "GetAssetByRfid")]
    [SwaggerResponse(200, "The asset was found and returned.", typeof(AssetResource))]
    [SwaggerResponse(404, "The asset was not found.")]
    public async Task<IActionResult> GetAssetByRfid(string rfidTagId)
    {
        var getAssetByRfidTagQuery = new GetAssetByRfidTagQuery(rfidTagId);
        var asset = await assetQueryService.Handle(getAssetByRfidTagQuery);
        if (asset is null) return NotFound();
        var assetResource = AssetResourceFromEntityAssembler.ToResourceFromEntity(asset);
        return Ok(assetResource);
    }

    /// <summary>
    ///     Get assets by responsible user
    /// </summary>
    [HttpGet("by-user/{responsibleUserId:int}")]
    [SwaggerOperation("Get Assets by Responsible User", 
        "Get all assets assigned to a specific user.", OperationId = "GetAssetsByUser")]
    [SwaggerResponse(200, "The assets were found and returned.", typeof(IEnumerable<AssetResource>))]
    [SwaggerResponse(404, "No assets were found for the user.")]
    public async Task<IActionResult> GetAssetsByUser(int responsibleUserId)
    {
        var getAssetsByUserQuery = new GetAssetsByResponsibleUserQuery(responsibleUserId);
        var assets = await assetQueryService.Handle(getAssetsByUserQuery);
        var assetResources = assets.Select(AssetResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(assetResources);
    }

    /// <summary>
    ///     Create a new asset
    /// </summary>
    [HttpPost]
    [SwaggerOperation("Create Asset", "Create a new asset.", OperationId = "CreateAsset")]
    [SwaggerResponse(201, "The asset was created.", typeof(AssetResource))]
    [SwaggerResponse(400, "The asset was not created.")]
    public async Task<IActionResult> CreateAsset(CreateAssetResource resource)
    {
        var createAssetCommand = CreateAssetCommandFromResourceAssembler.ToCommandFromResource(resource);
        var asset = await assetCommandService.Handle(createAssetCommand);
        if (asset is null) return BadRequest();
        var assetResource = AssetResourceFromEntityAssembler.ToResourceFromEntity(asset);
        return CreatedAtAction(nameof(GetAssetById), new { assetId = asset.Id }, assetResource);
    }

    /// <summary>
    ///     Get all assets
    /// </summary>
    [HttpGet]
    [SwaggerOperation("Get All Assets", "Get all assets.", OperationId = "GetAllAssets")]
    [SwaggerResponse(200, "The assets were found and returned.", typeof(IEnumerable<AssetResource>))]
    [SwaggerResponse(404, "The assets were not found.")]
    public async Task<IActionResult> GetAllAssets()
    {
        var getAllAssetsQuery = new GetAllAssetsQuery();
        var assets = await assetQueryService.Handle(getAllAssetsQuery);
        var assetResources = assets.Select(AssetResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(assetResources);
    }

    /// <summary>
    ///     Update asset location
    /// </summary>
    [HttpPatch("{assetId:int}/location")]
    [SwaggerOperation("Update Asset Location", "Update the location of an asset.", 
        OperationId = "UpdateAssetLocation")]
    [SwaggerResponse(200, "The asset location was updated.", typeof(AssetResource))]
    [SwaggerResponse(404, "The asset was not found.")]
    [SwaggerResponse(400, "The asset location was not updated.")]
    public async Task<IActionResult> UpdateAssetLocation(int assetId, UpdateAssetLocationResource resource)
    {
        var updateAssetLocationCommand = 
            UpdateAssetLocationCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
        var asset = await assetCommandService.Handle(updateAssetLocationCommand);
        if (asset is null) return NotFound();
        var assetResource = AssetResourceFromEntityAssembler.ToResourceFromEntity(asset);
        return Ok(assetResource);
    }

    /// <summary>
    ///     Update asset condition from IoT sensors
    /// </summary>
    [HttpPatch("{assetId:int}/condition")]
    [SwaggerOperation("Update Asset Condition", 
        "Update asset condition monitoring data from IoT sensors.", OperationId = "UpdateAssetCondition")]
    [SwaggerResponse(200, "The asset condition was updated.", typeof(AssetResource))]
    [SwaggerResponse(404, "The asset was not found.")]
    [SwaggerResponse(400, "The asset condition was not updated.")]
    public async Task<IActionResult> UpdateAssetCondition(int assetId, UpdateAssetConditionResource resource)
    {
        var updateAssetConditionCommand = 
            UpdateAssetConditionCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
        var asset = await assetCommandService.Handle(updateAssetConditionCommand);
        if (asset is null) return NotFound();
        var assetResource = AssetResourceFromEntityAssembler.ToResourceFromEntity(asset);
        return Ok(assetResource);
    }
}
