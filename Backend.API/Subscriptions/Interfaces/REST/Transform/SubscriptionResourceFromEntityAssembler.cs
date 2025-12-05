using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a SubscriptionResource from a Subscription entity
/// </summary>
public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        return new SubscriptionResource(
            entity.Id,
            entity.PlanId,
            entity.UserId,
            entity.Price.Amount,
            entity.Price.Currency,
            entity.Period.StartDate,
            entity.Period.EndDate,
            entity.PaymentReference.Value,
            entity.Status.Value
        );
    }
}