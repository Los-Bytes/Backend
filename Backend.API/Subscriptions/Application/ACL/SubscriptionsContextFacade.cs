using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;

namespace Backend.API.Subscriptions.Application.ACL;

/// <summary>
///     Facade for the subscriptions context
/// </summary>
public class SubscriptionsContextFacade(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService)
{
    /// <summary>
    ///     Initializes a default Free subscription for a new user
    /// </summary>
    public async Task<int> InitializeDefaultSubscription(int userId)
    {
        try
        {
            // Verificar si ya tiene suscripción activa
            var query = new GetActiveSubscriptionByUserIdQuery(userId);
            var existing = await subscriptionQueryService.Handle(query);
            if (existing != null) return existing.Id;

            // Crear suscripción Free
            var command = new CreateSubscriptionCommand(
                PlanId: 0,
                UserId: userId,
                Amount: 0m,
                Currency: "USD",
                StartDate: DateTime.UtcNow,
                EndDate: DateTime.UtcNow.AddYears(100),
                PaymentReference: $"FREE-INIT-{userId}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Status: "Active"
            );

            var subscription = await subscriptionCommandService.Handle(command);
            return subscription?.Id ?? 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    /// <summary>
    ///     Fetches the active subscription for a user
    /// </summary>
    public async Task<int> FetchActiveSubscriptionIdByUserId(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        return subscription?.Id ?? 0;
    }

    /// <summary>
    ///     Changes user subscription plan
    /// </summary>
    public async Task<bool> ChangeUserSubscriptionPlan(int userId, string newPlanType)
    {
        var command = new ChangeSubscriptionPlanCommand(userId, newPlanType);
        var subscription = await subscriptionCommandService.Handle(command);
        return subscription != null;
    }

    /// <summary>
    ///     Checks if user subscription is currently active
    /// </summary>
    public async Task<bool> IsUserSubscriptionActive(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        return subscription?.IsCurrentlyActive() ?? false;
    }

    /// <summary>
    ///     Gets remaining days of subscription
    /// </summary>
    public async Task<int> GetRemainingDays(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        return subscription?.GetRemainingDays() ?? 0;
    }

    /// <summary>
    ///     Checks if subscription is free
    /// </summary>
    public async Task<bool> IsFreePlan(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        return subscription?.Price.IsFree() ?? true;
    }
}
