using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;

namespace Backend.API.Subscriptions.Application.ACL;

/// <summary>
///     Facade for the subscriptions context
/// </summary>
/// <param name="subscriptionCommandService">
///     The subscription command service
/// </param>
/// <param name="subscriptionQueryService">
///     The subscription query service
/// </param>
/// <param name="subscriptionPlanQueryService">
///     The subscription plan query service
/// </param>
public class SubscriptionsContextFacade(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService,
    ISubscriptionPlanQueryService subscriptionPlanQueryService)
{
    /// <summary>
    ///     Initializes a default Free subscription for a new user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>The subscription identifier</returns>
    public async Task<int> InitializeDefaultSubscription(int userId)
    {
        var subscription = await subscriptionCommandService.InitializeDefaultSubscription(userId);
        return subscription?.Id ?? 0;
    }

    /// <summary>
    ///     Fetches the active subscription for a user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>The subscription identifier</returns>
    public async Task<int> FetchActiveSubscriptionIdByUserId(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        return subscription?.Id ?? 0;
    }

    /// <summary>
    ///     Changes user subscription plan
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="newPlanType">The new plan type (Free, Pro, Max)</param>
    /// <returns>True if successful, false otherwise</returns>
    public async Task<bool> ChangeUserSubscriptionPlan(int userId, string newPlanType)
    {
        var command = new ChangeSubscriptionPlanCommand(userId, newPlanType);
        var subscription = await subscriptionCommandService.Handle(command);
        return subscription != null;
    }

    /// <summary>
    ///     Fetches the current limits for a user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>Tuple with (maxMembers, maxInventoryItems)</returns>
    public async Task<(int maxMembers, int maxInventoryItems)> FetchUserLimits(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        
        if (subscription == null)
            return (3, 50); // Default Free plan limits
        
        return (subscription.MaxMembers, subscription.MaxInventoryItems);
    }
}
