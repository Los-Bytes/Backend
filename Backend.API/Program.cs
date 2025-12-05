using Backend.API.IAM.Infrastructure.Pipeline.Middleware.Extensions; 
using Backend.API.History.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Profiles.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Inventory.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Backend.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.Subscriptions.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Laboratories.Infrastructure.Interfaces.ASP.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "http://localhost:3000",
            "https://frontendwebapplications-7x8z.onrender.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Database Configuration
builder.AddDatabaseConfigurationServices();

// OpenAPI/Swagger Configuration
builder.AddOpenApiConfigurationServices();

// Dependency Injection for Bounded Contexts
//builder.AddSharedContextServices();
builder.AddUserProfilesContextServices();
builder.AddInventoryContextServices();
builder.AddIamContextServices();
builder.AddHistoryContextServices();
builder.AddSubscriptionsContextServices();
builder.AddLaboratoryContextServices();

// Mediator Configuration
builder.AddCortexMediatorServices();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
app.EnsureDatabaseCreated();

// Configure OpenAPI/Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
// CORS Middleware
app.UseCors("AllowFrontend");

app.UseRequestAuthorization();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
