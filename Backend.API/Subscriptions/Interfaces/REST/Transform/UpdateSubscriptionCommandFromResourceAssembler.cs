using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an UpdateSubscriptionCommand from a resource
/// </summary>
public static class UpdateSubscriptionCommandFromResourceAssembler
{
    public static UpdateSubscriptionCommand ToCommandFromResource(int id, UpdateSubscriptionResource resource)
    {
        return new UpdateSubscriptionCommand(
            id,
            resource.Amount,
            resource.Currency,
            resource.IsActive
        );
    }
}