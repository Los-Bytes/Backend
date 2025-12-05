using Microsoft.EntityFrameworkCore;
using Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration;

namespace Backend.API.Subscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class SubscriptionsModelBuilderExtensions
{
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new SubscriptionConfiguration());
        builder.ApplyConfiguration(new SubscriptionPlanConfiguration());
    }
}