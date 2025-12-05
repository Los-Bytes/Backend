using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

public static class UpdateSubscriptionCommandFromResourceAssembler
{
    public static UpdateSubscriptionCommand ToCommandFromResource(int id, UpdateSubscriptionResource resource)
    {
        return new UpdateSubscriptionCommand(
            id,
            resource.MaxMembers,
            resource.MaxInventoryItems,
            resource.IsActive
        );
    }
}