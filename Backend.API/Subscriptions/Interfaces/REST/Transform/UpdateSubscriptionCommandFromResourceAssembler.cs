using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an UpdateSubscriptionCommand from a resource
/// </summary>
public static class UpdateSubscriptionCommandFromResourceAssembler
{
    /// <summary>
    ///     Create an UpdateSubscriptionCommand from a resource
    /// </summary>
    /// <param name="id">The subscription identifier</param>
    /// <param name="resource">The <see cref="UpdateSubscriptionResource" /> resource</param>
    /// <returns>The <see cref="UpdateSubscriptionCommand" /> command</returns>
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