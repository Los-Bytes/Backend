using Backend.API.IAM.Infrastructure.Pipeline.Middleware.Extensions; 
using Backend.API.History.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Profiles.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Inventory.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Backend.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

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


// Mediator Configuration
builder.AddCortexMediatorServices();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
app.EnsureDatabaseCreated();

// Configure OpenAPI/Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRequestAuthorizationMiddleware();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
