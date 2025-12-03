using Backend.API.History.Application.Internal.CommandServices;
using Backend.API.History.Application.Internal.QueryServices;
using Backend.API.History.Domain.Repositories;
using Backend.API.History.Domain.Services;
using Backend.API.History.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.History.Infrastructure.Interfaces.ASP.Configuration.Extensions
{
    /// <summary>
    /// Dependency injection configuration for the History bounded context.
    /// </summary>
    public static class HistoryContextServicesExtensions
    {
        /// <summary>
        /// Adds History bounded context services to the application builder.
        /// </summary>
        public static void AddHistoryContextServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            // Repositories
            services.AddScoped<IHistoryEntryRepository, HistoryEntryRepository>();

            // Command & Query Services
            services.AddScoped<IHistoryEntryCommandService, HistoryEntryCommandService>();
            services.AddScoped<IHistoryEntryQueryService, HistoryEntryQueryService>();
        }
    }
}