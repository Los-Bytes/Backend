using Backend.API.Profiles.Application.Internal.CommandServices;
using Backend.API.Profiles.Application.Internal.QueryServices;
using Backend.API.Profiles.Domain.Repositories;
using Backend.API.Profiles.Domain.Services;
using Backend.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Profiles.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Web Application Builder Extensions for Profiles Context
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Registers the user profiles context services
    /// </summary>
    /// <param name="builder">The web application builder</param>
    public static void AddUserProfilesContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        builder.Services.AddScoped<IUserProfileCommandService, UserProfileCommandService>();
        builder.Services.AddScoped<IUserProfileQueryService, UserProfileQueryService>();
    }
}