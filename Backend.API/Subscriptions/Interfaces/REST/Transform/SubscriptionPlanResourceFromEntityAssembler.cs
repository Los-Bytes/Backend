using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

public static class SubscriptionPlanResourceFromEntityAssembler
{
    public static SubscriptionPlanResource ToResourceFromEntity(SubscriptionPlan entity)
    {
        return new SubscriptionPlanResource(
            entity.Id,
            entity.Name,
            entity.Price,
            entity.Currency,
            entity.Period,
            entity.MaxMembers,
            entity.MaxInventoryItems,
            entity.GetFeaturesList()
        );
    }
}