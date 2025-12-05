using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Commands;

namespace Backend.API.Subscriptions.Domain.Services;

/// <summary>
///     Subscription command service interface
/// </summary>
public interface ISubscriptionCommandService
{
    /// <summary>
    ///     Handle create subscription command
    /// </summary>
    /// <param name="command">The <see cref="CreateSubscriptionCommand" /> command</param>
    /// <returns>The <see cref="Subscription" /> object with the created subscription</returns>
    Task<Subscription?> Handle(CreateSubscriptionCommand command);

    /// <summary>
    ///     Handle update subscription command
    /// </summary>
    /// <param name="command">The <see cref="UpdateSubscriptionCommand" /> command</param>
    /// <returns>The <see cref="Subscription" /> object with the updated subscription</returns>
    Task<Subscription?> Handle(UpdateSubscriptionCommand command);

    /// <summary>
    ///     Handle change subscription plan command
    /// </summary>
    /// <param name="command">The <see cref="ChangeSubscriptionPlanCommand" /> command</param>
    /// <returns>The <see cref="Subscription" /> object with the new subscription</returns>
    Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command);

    /// <summary>
    ///     Initialize default Free subscription for a new user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>The <see cref="Subscription" /> object with the created subscription</returns>
    Task<Subscription?> InitializeDefaultSubscription(int userId);
}