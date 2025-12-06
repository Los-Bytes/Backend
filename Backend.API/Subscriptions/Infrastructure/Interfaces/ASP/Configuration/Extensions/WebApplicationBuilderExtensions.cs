using Backend.API.Subscriptions.Application.ACL;
using Backend.API.Subscriptions.Application.Internal.CommandServices;
using Backend.API.Subscriptions.Application.Internal.QueryServices;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

namespace Backend.API.Subscriptions.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Web Application Builder Extensions for Subscriptions Context
/// </summary>
public static class WebApplicationBuilderExtensions
{
    public static void AddSubscriptionsContextServices(this WebApplicationBuilder builder)
    {
        // Repositories
        builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        
        // Services
        builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
        builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
        
        // ACL Facade
        builder.Services.AddScoped<SubscriptionsContextFacade>();
    }
}