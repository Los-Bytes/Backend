using Backend.API.History.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.Inventory.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Backend.API.Laboratories.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for LabIoT Backend
/// </summary>
/// <remarks>
///     This context integrates two bounded contexts: Profiles and Inventory
/// </remarks>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     On configuring the database context
    /// </summary>
    /// <remarks>
    ///     This method is used to configure the database context.
    ///     It also adds the created and updated date interceptor to the database context.
    /// </remarks>
    /// <param name="builder">
    ///     The option builder for the database context
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Profiles Context
        builder.ApplyUserProfilesConfiguration();

        // Inventory Context
        builder.ApplyInventoryConfiguration();
        
        // IAM Context
        builder.ApplyIamConfiguration();
        
        // History Context
        builder.ApplyHistoryConfiguration();
        
        // Subscriptions Context
        builder.ApplySubscriptionsConfiguration();
        
        //Laboratory Context
        builder.ApplyLaboratoryConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
