using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(
            resource.UserId,
            resource.PlanType,
            resource.StartDate,
            resource.EndDate,
            resource.MaxMembers,
            resource.MaxInventoryItems,
            resource.IsActive
        );
    }
}