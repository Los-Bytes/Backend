using Microsoft.EntityFrameworkCore;
using Backend.API.Shared.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddDatabaseConfigurationServices(this WebApplicationBuilder builder)
    {
        // Desarrollo: usa directamente la connection string del appsettings.Development
        if (builder.Environment.IsDevelopment())
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found for Development.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());
        }
        // Producción: toma la plantilla y reemplaza %VARIABLES%
        else if (builder.Environment.IsProduction())
        {
            // Carga config de appsettings.Production + variables de entorno
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                    optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionStringTemplate = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionStringTemplate))
                throw new Exception("Database connection string template 'DefaultConnection' is not set in the configuration.");

            // Reemplaza %DATABASE_URL%, %DATABASE_PORT%, etc. con los valores del entorno
            var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Database connection string is empty after expanding environment variables.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors());
        }
        else
        {
            throw new Exception($"Environment '{builder.Environment.EnvironmentName}' not supported.");
        }

        // Shared
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
