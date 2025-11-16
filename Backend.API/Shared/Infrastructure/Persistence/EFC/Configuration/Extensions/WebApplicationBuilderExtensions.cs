using Microsoft.EntityFrameworkCore;
using Backend.API.Shared.Domain.Repositories;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddDatabaseConfigurationServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error);
        });
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}