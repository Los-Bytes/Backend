using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Interfaces.REST.Resources;

namespace Backend.API.Subscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a ChangeSubscriptionPlanCommand from a resource
/// </summary>
public static class ChangeSubscriptionPlanCommandFromResourceAssembler
{
    /// <summary>
    ///     Create a ChangeSubscriptionPlanCommand from a resource
    /// </summary>
    /// <param name="resource">The <see cref="ChangeSubscriptionPlanResource" /> resource</param>
    /// <returns>The <see cref="ChangeSubscriptionPlanCommand" /> command</returns>
    public static ChangeSubscriptionPlanCommand ToCommandFromResource(ChangeSubscriptionPlanResource resource)
    {
        return new ChangeSubscriptionPlanCommand(
            resource.UserId,
            resource.NewPlanType
        );
    }
}