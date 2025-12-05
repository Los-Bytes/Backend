using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a CreateSubscriptionCommand from a resource
/// </summary>
public static class CreateSubscriptionCommandFromResourceAssembler
{
    /// <summary>
    ///     Create a CreateSubscriptionCommand from a resource
    /// </summary>
    /// <param name="resource">The <see cref="CreateSubscriptionResource" /> resource</param>
    /// <returns>The <see cref="CreateSubscriptionCommand" /> command</returns>
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