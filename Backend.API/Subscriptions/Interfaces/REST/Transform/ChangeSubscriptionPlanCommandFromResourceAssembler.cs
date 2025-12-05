using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

public static class ChangeSubscriptionPlanCommandFromResourceAssembler
{
    public static ChangeSubscriptionPlanCommand ToCommandFromResource(ChangeSubscriptionPlanResource resource)
    {
        return new ChangeSubscriptionPlanCommand(
            resource.UserId,
            resource.NewPlanType
        );
    }
}