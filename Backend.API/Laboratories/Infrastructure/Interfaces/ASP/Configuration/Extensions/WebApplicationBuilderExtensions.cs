using Backend.API.Laboratories.Application.ACL;
using Backend.API.Laboratories.Application.Internal.CommandServices;
using Backend.API.Laboratories.Application.Internal.QueryServices;
using Backend.API.Laboratories.Domain.Repositories;
using Backend.API.Laboratories.Domain.Services;
using Backend.API.Laboratories.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Laboratories.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Web Application Builder Extensions for Laboratory Context
/// </summary>
public static class WebApplicationBuilderExtensions
{
    public static void AddLaboratoryContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
        builder.Services.AddScoped<ILaboratoryCommandService, LaboratoryCommandService>();
        builder.Services.AddScoped<ILaboratoryQueryService, LaboratoryQueryService>();
        builder.Services.AddScoped<LaboratoryContextFacade>();
    }
}