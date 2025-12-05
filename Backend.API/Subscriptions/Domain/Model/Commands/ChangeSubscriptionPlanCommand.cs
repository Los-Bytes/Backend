namespace Backend.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Change Subscription Plan Command
/// </summary>
/// <param name="UserId">The user identifier</param>
/// <param name="NewPlanType">The new plan type (Free, Pro, Max)</param>
public record ChangeSubscriptionPlanCommand(
    int UserId,
    string NewPlanType);