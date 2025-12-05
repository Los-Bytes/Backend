using Backend.API.Shared.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// WebApplicationBuilder extensions for database configuration.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the database configuration services for the application.
    /// Uses the "DefaultConnection" connection string from configuration
    /// for both Development and Production.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddDatabaseConfigurationServices(this WebApplicationBuilder builder)
    {
        // Lee la cadena de conexión desde appsettings.{Environment}.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Configura AppDbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine,
                    builder.Environment.IsDevelopment()
                        ? LogLevel.Information   // más verbose en dev
                        : LogLevel.Error)        // solo errores en prod
                .EnableDetailedErrors();

            if (builder.Environment.IsDevelopment())
            {
                // Solo en desarrollo para evitar exponer datos sensibles en producción
                options.EnableSensitiveDataLogging();
            }
        });

        // UnitOfWork compartido
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}