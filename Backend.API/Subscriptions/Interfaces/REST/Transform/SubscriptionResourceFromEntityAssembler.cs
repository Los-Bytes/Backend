using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a SubscriptionResource from a Subscription entity
/// </summary>
public static class SubscriptionResourceFromEntityAssembler
{
    /// <summary>
    ///     Create a SubscriptionResource from a Subscription entity
    /// </summary>
    /// <param name="entity">The <see cref="Subscription" /> entity</param>
    /// <returns>The <see cref="SubscriptionResource" /> resource</returns>
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