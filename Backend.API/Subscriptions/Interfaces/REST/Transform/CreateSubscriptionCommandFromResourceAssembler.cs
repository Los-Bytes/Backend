using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a CreateSubscriptionCommand from a resource
/// </summary>
public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(
            resource.PlanId,
            resource.UserId,
            resource.Amount,
            resource.Currency,
            resource.StartDate,
            resource.EndDate,
            resource.PaymentReference,
            resource.Status
        );
    }
}