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
    /// <summary>
    ///     Registers the subscriptions context services
    /// </summary>
    /// <param name="builder">The web application builder</param>
    public static void AddSubscriptionsContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
        builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
        builder.Services.AddScoped<ISubscriptionPlanQueryService, SubscriptionPlanQueryService>();
    }
}