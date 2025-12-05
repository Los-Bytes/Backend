using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        return new SubscriptionResource(
            entity.Id,
            entity.UserId,
            entity.PlanType,
            entity.StartDate,
            entity.EndDate,
            entity.MaxMembers,
            entity.MaxInventoryItems,
            entity.IsActive
        );
    }
}