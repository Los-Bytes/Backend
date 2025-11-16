using Backend.API.Shared.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSharedContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}