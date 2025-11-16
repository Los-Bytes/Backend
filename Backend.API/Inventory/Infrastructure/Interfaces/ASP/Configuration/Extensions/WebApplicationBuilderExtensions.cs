using Backend.API.Inventory.Application.Internal.CommandServices;
using Backend.API.Inventory.Application.Internal.QueryServices;
using Backend.API.Inventory.Domain.Repositories;
using Backend.API.Inventory.Domain.Services;
using Backend.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Inventory.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Web Application Builder Extensions for Inventory Context
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Registers the inventory context services
    /// </summary>
    /// <param name="builder">The web application builder</param>
    public static void AddInventoryContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAssetRepository, AssetRepository>();
        builder.Services.AddScoped<IAssetCommandService, AssetCommandService>();
        builder.Services.AddScoped<IAssetQueryService, AssetQueryService>();
    }
}