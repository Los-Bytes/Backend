using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a SubscriptionPlanResource from a SubscriptionPlan entity
/// </summary>
public static class SubscriptionPlanResourceFromEntityAssembler
{
    /// <summary>
    ///     Create a SubscriptionPlanResource from a SubscriptionPlan entity
    /// </summary>
    /// <param name="entity">The <see cref="SubscriptionPlan" /> entity</param>
    /// <returns>The <see cref="SubscriptionPlanResource" /> resource</returns>
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