namespace Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class WebApplicationExtensions
{
    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        // Verify if the database exists and create it if it doesn't
        app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
    }
}